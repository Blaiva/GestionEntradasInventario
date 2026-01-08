using GestionEntradasInventario.Data;
using GestionEntradasInventario.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionEntradasInventario.Services;

public class EntradasService(IDbContextFactory<ApplicationDbContext> dbContext)
{
    public async Task<bool> Existe(int entradaId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Entradas.AnyAsync(e => e.EntradaId == entradaId);
    }

    public async Task AfectarExistencia(EntradaDetalles[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        foreach(var item in detalle)
        {
            var producto = await contexto.Productos.SingleAsync(p => p.ProductoId == item.ProductoId);
            if (tipoOperacion == TipoOperacion.Suma)
                producto.Existencia += item.Cantidad;
            else
                producto.Existencia -= item.Cantidad;
            await contexto.SaveChangesAsync();
        }
    }

    public async Task<bool> Insertar(Entradas entrada)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.Entradas.Add(entrada);
        await AfectarExistencia(entrada.EntradaDetalles.ToArray(), TipoOperacion.Suma);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Entradas entrada)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        var original = await contexto.Entradas.Include(e => e.EntradaDetalles).AsNoTracking().SingleOrDefaultAsync(e => e.EntradaId == entrada.EntradaId);

        if(original == null) return false;

        await AfectarExistencia(original.EntradaDetalles.ToArray(), TipoOperacion.Resta);
        contexto.EntradaDetalles.RemoveRange(original.EntradaDetalles);

        contexto.Update(entrada);
        await AfectarExistencia(entrada.EntradaDetalles.ToArray(), TipoOperacion.Suma);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Entradas entrada)
    {
        if(!await Existe(entrada.EntradaId))
            return await Insertar(entrada);
        else
            return await Modificar(entrada);
    }

    public async Task<Entradas?> Buscar(int entradaId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Entradas.Include(e => e.EntradaDetalles).FirstOrDefaultAsync(e => e.EntradaId == entradaId);
    }

    public async Task<bool> Eliminar(int entradaId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        var entrada = await Buscar(entradaId);

        await AfectarExistencia(entrada.EntradaDetalles.ToArray(), TipoOperacion.Resta);
        contexto.EntradaDetalles.RemoveRange(entrada.EntradaDetalles);
        contexto.Entradas.Remove(entrada);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Entradas>> Listar(Expression<Func<Entradas, bool>> criterio)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Entradas.Include(e => e.EntradaDetalles).Where(criterio).AsNoTracking().ToListAsync();
    }
}

public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}