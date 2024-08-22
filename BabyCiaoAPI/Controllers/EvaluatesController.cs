using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Cors;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluatesController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly Evaluate _evaluate;

        public EvaluatesController(BabyciaoContext context, Evaluate evaluate)
        {
            _context = context;
            _evaluate = evaluate;
        }


        [HttpGet]
        public ActionResult<IEnumerable<UserEvaluationDTO>> GetUserEvaluations()
        {
            var evaluations = _context.Evaluates
                .Where(e => e.Display) // Only display evaluations marked for display
                .Join(_context.UserInformations,
                      eval => eval.AppraiseeUserAccount,
                      userInfo => userInfo.AccountUser,
                      (eval, userInfo) => new UserEvaluationDTO
                      {
                          Nickname = userInfo.Nickname,
                          UserPhoto = userInfo.UserPhoto,
                          Score = eval.Score,
                          Memo = eval.Memo
                      })
                .ToList();

            return Ok(evaluations);
        }

        // GET: api/UserEvaluations/5
        [HttpGet("{id}")]
        public ActionResult<UserEvaluationDTO> GetUserEvaluation(int id)
        {
            var evaluation = _context.Evaluates
                .Where(e => e.Id == id && e.Display) // Ensure the evaluation exists and is marked for display
                .Join(_context.UserInformations,
                      eval => eval.AppraiseeUserAccount,
                      userInfo => userInfo.AccountUser,
                      (eval, userInfo) => new UserEvaluationDTO
                      {
                          Nickname = userInfo.Nickname,
                          UserPhoto = userInfo.UserPhoto,
                          Score = eval.Score,
                          Memo = eval.Memo
                      })
                .FirstOrDefault();

            if (evaluation == null)
            {
                return NotFound();
            }

            return Ok(evaluation);
        }

        // POST: api/UserEvaluations
        //[HttpPost]
        //public ActionResult<UserEvaluationDTO> PostUserEvaluation([FromBody] EvaluateDTO evaluation)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Evaluates.Add(evaluation);
        //    _context.SaveChanges();

        //    var userEvaluation = _context.UserInformations
        //        .Where(userInfo => userInfo.AccountUser == evaluation.AppraiseeUserAccount)
        //        .Select(userInfo => new UserEvaluationDTO
        //        {
        //            Nickname = userInfo.Nickname,
        //            UserPhoto = userInfo.UserPhoto,
        //            Score = evaluation.Score,
        //            Memo = evaluation.Memo
        //        })
        //        .FirstOrDefault();

        //    return CreatedAtAction(nameof(GetUserEvaluation), new { id = evaluation.Id }, userEvaluation);
        //}

        // PUT: api/UserEvaluations/5
        [HttpPut("{id}")]
        public IActionResult PutUserEvaluation(int id, [FromBody] EvaluateDTO evaluation)
        {
            if (id != evaluation.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingEvaluation = _context.Evaluates.Find(id);
            if (existingEvaluation == null)
            {
                return NotFound();
            }

            existingEvaluation.Score = evaluation.Score;
            existingEvaluation.Memo = evaluation.Memo;
            existingEvaluation.Display = evaluation.Display;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/UserEvaluations/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUserEvaluation(int id)
        {
            var evaluation = _context.Evaluates.Find(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            _context.Evaluates.Remove(evaluation);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

//        // GET: api/Evaluates
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<EvaluateDTO>>> GetEvaluates()
//        {
//            return await _context.Evaluates.Select(c => new EvaluateDTO
//            {
//                Id=c.Id,
//                AppraiseeUserAccount=c.AppraiseeUserAccount,
//                Score=c.Score,
//                Memo=c.Memo,
//            }).ToListAsync();
//        }



//        // GET: api/Evaluates/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Evaluate>> GetEvaluateinfo(int id)
//        {
//            try
//            {
//                var evaluate = await _context.Evaluates.Select(c => new EvaluateDTO
//                {
//                    Id = c.Id,
//                    EvaluatorUserAccount = c.EvaluatorUserAccount,
//                    AppraiseeUserAccount = c.AppraiseeUserAccount,
//                    EvaluateTime = c.EvaluateTime,
//                    Score = c.Score,
//                    Memo = c.Memo,
//                    Display = c.Display,
//                }).ToListAsync();


//                return Ok(evaluate);
//            }
//            catch (Exception ex)
//            {
//                // Log the exception
//                Console.WriteLine(ex.Message);
//                return StatusCode(500, "Internal server error");
//            }
//        }


//// PUT: api/Evaluates/5
//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//[HttpPut("{id}")]
//        public async Task<IActionResult> PutEvaluate(int id, Evaluate evaluate)
//        {
//            if (id != evaluate.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(evaluate).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!EvaluateExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Evaluates
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Evaluate>> PostEvaluate(Evaluate evaluate)
//        {
//            _context.Evaluates.Add(evaluate);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetEvaluate", new { id = evaluate.Id }, evaluate);
//        }

//        // DELETE: api/Evaluates/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEvaluate(int id)
//        {
//            var evaluate = await _context.Evaluates.FindAsync(id);
//            if (evaluate == null)
//            {
//                return NotFound();
//            }

//            _context.Evaluates.Remove(evaluate);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool EvaluateExists(int id)
//        {
//            return _context.Evaluates.Any(e => e.Id == id);
//        }
//    }

