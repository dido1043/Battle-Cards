using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCards.ViewModels
{
    public class CardsViewModel
    {
        public int CardId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Keyword { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
    }
}
