
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreMVC.Models;

public class EnrollmentsController : Controller
{
    private readonly SchoolContext _context;

    public EnrollmentsController(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Enrollments.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(m => m.EnrollmentID == id);

        if (enrollment == null)
            return NotFound();

        return View(enrollment);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(enrollment);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var enrollment = await _context.Enrollments.FindAsync(id);

        if (enrollment == null)
            return NotFound();

        return View(enrollment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
    {
        if (id != enrollment.EnrollmentID)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(enrollment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(enrollment.EnrollmentID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(enrollment);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(m => m.EnrollmentID == id);

        if (enrollment == null)
            return NotFound();

        return View(enrollment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);

        if (enrollment != null)
            _context.Enrollments.Remove(enrollment);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EnrollmentExists(int? id)
    {
        return _context.Enrollments.Any(e => e.EnrollmentID == id);
    }
}