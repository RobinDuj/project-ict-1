using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_ict_thomas_is_git
{
    internal class Score
    {
        int score = 0;

        public string Verhogen()
        {
            score++;
            return score.ToString();
        }
        public string Null()
        {
            score = 0;
            return score.ToString();
        }
        public string Show()
        {
            return $"Score: {score}";
        }
    }
}
