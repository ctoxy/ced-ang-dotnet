namespace DatingApp.API.Models
{
    public class Like
    {   
        /* id d'un user qui aime  un autre user */
        public int LikerId { get; set; }
        /* id d'un user qui est aimer par un/plusieurs autres  user */
        public int LikeeId { get; set; }
        /* un user qui aime par un autre user */
        public  User Liker { get; set; }
        /* un user aqui est aimer par un/plusieurs autres  user */
        public  User Likee { get; set; }
    }
}