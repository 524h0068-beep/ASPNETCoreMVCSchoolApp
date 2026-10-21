
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreMVC.Models;

public class InstructorsController : Controller
{
    private readonly SchoolContext _context;

    public InstructorsController(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Instructors.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var instructor = await _context.Instructors
            .FirstOrDefaultAsync(m => m.InstructorID == id);

        if (instructor == null)
            return NotFound();

        return View(instructor);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("InstructorID,FirstName,LastName,HireDate")] Instructor instructor)
    {
        if (ModelState.IsValid)
        {
            _context.Add(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(instructor);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var instructor = await _context.Instructors.FindAsync(id);

        if (instructor == null)
            return NotFound();

        return View(instructor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("InstructorID,FirstName,LastName,HireDate")] Instructor instructor)
    {
        if (id != instructor.InstructorID)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(instructor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(instructor.InstructorID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(instructor);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var instructor = await _context.Instructors
            .FirstOrDefaultAsync(m => m.InstructorID == id);

        if (instructor == null)
            return NotFound();

        return View(instructor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var instructor = await _context.Instructors.FindAsync(id);

        if (instructor != null)
            _context.Instructors.Remove(instructor);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InstructorExists(int? id)
    {
        return _context.Instructors.Any(e => e.InstructorID == id);
    }
}