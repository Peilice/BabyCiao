using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

    public class CompetitionFavoriteDTO
    {
        public int FavoriteId { get; set; }

        public string CompetitionName { get; set; }

        public string myAccount { get; set; }

        public int CompetitionId {  get; set; }

    }

    public class CompetitionFavorite_createDTO
    {

        public string myAccount { get; set; }

        public int CompetitionId { get; set; }

    }
