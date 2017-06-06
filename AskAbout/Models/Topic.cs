using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AskAbout.Models
{
    public class Topic
    {
        [Key]
        [Required(ErrorMessageResourceName = "Select",
            ErrorMessageResourceType = typeof(Resources.Models.Topic))]
        public string Name { get; set; }

        public virtual List<Question> Questions { get; set; }
        public virtual List<Rating> Rating { get; set; }
    }
}