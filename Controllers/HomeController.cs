using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Barley_Break.Models;

namespace Barley_Break.Controllers
{
    public class HomeController : Controller
    {
        static BarleyModel staticModel = new BarleyModel();
        const int square = 4;
        static List<int> template = new List<int>();
        bool swaped;

        /// <summary>
        /// Initialize some data to start game 
        /// <returns></returns>
        public ActionResult NewGame()
        {
            staticModel.Sequence.Clear();
            template.Clear();
            //initialize for start
            Random r = new Random();
            for (int i = 0; i < square * square; i++)
            {
                int rand = r.Next(square * square);
                while (staticModel.Sequence.Contains(rand)) rand = r.Next(0, square * square);
                staticModel.Sequence.Add(rand);
                template.Add(i);
            }

            //use this to check Congratulations
            //staticModel.Sequence = new List<int> {1,0,2,3,4,5,6,7,8,9,10,11,12,13,14,15 };
            return RedirectToAction("Index");
        }

        /// <summary>
        /// check if the game is over and you won
        /// otherwise return game View
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
                if (staticModel.Sequence.SequenceEqual(template))
                    return RedirectToAction("Congratulations");
            
            return View(staticModel);
        }

        /// <summary>
        /// This method swaps values 'a' and 'b' in Sequence property of BarleyModel class
        /// </summary>
        /// <param name="barleyModel">the instance of class to swap values</param>
        /// <param name="a">first value to swap</param>
        /// <param name="b">second value to swap</param>
        private void Swap(ref BarleyModel barleyModel, int a, int b)
        {
            //swap
            int temp = barleyModel.Sequence[a];
            barleyModel.Sequence[a] = barleyModel.Sequence[b];
            barleyModel.Sequence[b] = temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Congratulations()
        {
            staticModel.Sequence.RemoveRange(0,square * square);
            return View();
        }

        /// <summary>
        /// Check the position of button you pressed and 'space' button and swap them, if they are neighbors
        /// </summary>
        /// <param name="barleyModel">the ionstance to collect data from View</param>
        /// <param name="barleyModel.Button">a value of button you pressed</param>
        /// <param name="barleyModel.Sequence">current sequence of buttons</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Shuffle(BarleyModel barleyModel)
        {
            int zeroPosition = barleyModel.Sequence.IndexOf(0);
            int btnPos = barleyModel.Sequence.IndexOf(barleyModel.Button);
            if ((zeroPosition+1) % square != 0)
            {
                if (btnPos - zeroPosition == 1)
                {
                    Swap(ref barleyModel, zeroPosition, btnPos);
                    swaped = true;
                }
                else if ((zeroPosition+1 <= (barleyModel.Sequence.Count - square) && btnPos - zeroPosition == 4) || (zeroPosition+1 > square && zeroPosition - btnPos == 4))
                {
                    Swap(ref barleyModel, zeroPosition, btnPos);
                    swaped = true;
                }

            }
            if (!swaped)
            {
                if (zeroPosition + 4 % square != 0)
                {
                    if (zeroPosition - btnPos == 1)
                    {
                        Swap(ref barleyModel, zeroPosition, btnPos);
                    }
                    else if ((zeroPosition+1 <= (barleyModel.Sequence.Count - square) && btnPos - zeroPosition == 4) || (zeroPosition+1 > square && zeroPosition - btnPos == 4))
                    {
                        Swap(ref barleyModel, zeroPosition, btnPos);
                    }

                }
            }
            swaped = false;

            staticModel = barleyModel;
            return RedirectToAction("Index","Home");
        }
    }
}