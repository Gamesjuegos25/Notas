using System.Linq;
using System.Web.Http;
using Tarea2p2.Models;


public class NotasController : ApiController
{
    private readonly ApplicationDbContext db = new ApplicationDbContext();

    // GET /api/notas
    public IHttpActionResult Get()
    {
        var notas = db.Notas.ToList(); // Obtiene todas las notas de la base de datos
        return Ok(notas); // Devuelve todas las notas con una respuesta 200 OK
    }

    // GET /api/notas/{id}
    public IHttpActionResult Get(int id)
    {
        var nota = db.Notas.FirstOrDefault(n => n.Id == id); // Busca la nota por ID en la base de datos
        if (nota == null)
        {
            return NotFound(); // 404 si no se encuentra la nota
        }
        return Ok(nota); // 200 OK si se encuentra la nota
    }

    // POST /api/notas
    public IHttpActionResult Post([FromBody] Nota nuevaNota)
    {
        if (nuevaNota == null || string.IsNullOrEmpty(nuevaNota.Titulo))
        {
            return BadRequest("El título de la nota es obligatorio."); // 400 si el título está vacío o nulo
        }

        // Agrega la nueva nota a la base de datos
        db.Notas.Add(nuevaNota);
        db.SaveChanges(); // Guarda los cambios en la base de datos

        return CreatedAtRoute("DefaultApi", new { id = nuevaNota.Id }, nuevaNota); // 201 Created
    }

    // PUT /api/notas/{id}
    public IHttpActionResult Put(int id, [FromBody] Nota notaActualizada)
    {
        if (notaActualizada == null || string.IsNullOrEmpty(notaActualizada.Titulo))
        {
            return BadRequest("El título de la nota es obligatorio."); // 400 si el título está vacío o nulo
        }

        var notaExistente = db.Notas.FirstOrDefault(n => n.Id == id); // Busca la nota existente en la base de datos
        if (notaExistente == null)
        {
            return NotFound(); // 404 si no se encuentra la nota
        }

        // Actualiza los campos de la nota
        notaExistente.Titulo = notaActualizada.Titulo;
        notaExistente.Contenido = notaActualizada.Contenido;
        db.SaveChanges(); // Guarda los cambios en la base de datos

        return Ok(notaExistente); // 200 OK si la nota es actualizada exitosamente
    }

    // DELETE /api/notas/{id}
    public IHttpActionResult Delete(int id)
    {
        var nota = db.Notas.FirstOrDefault(n => n.Id == id); // Busca la nota por ID en la base de datos
        if (nota == null)
        {
            return NotFound(); // 404 si no se encuentra la nota
        }

        // Elimina la nota de la base de datos
        db.Notas.Remove(nota);
        db.SaveChanges(); // Guarda los cambios en la base de datos

        return StatusCode(System.Net.HttpStatusCode.NoContent); // 204 No Content si la eliminación fue exitosa
    }
}
