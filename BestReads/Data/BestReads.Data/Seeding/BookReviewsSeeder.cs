using BestReads.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data.Seeding
{
    public class BookReviewsSeeder : ISeeder
    {
        private static ICollection<BookReview> bookReviews = new List<BookReview>
        {
            new BookReview { Title = "The Man Who Thought Different", Description = "A compelling story of Steve Jobs’ passion, innovation, and relentless pursuit of perfection.", Rating = 5, UserId = 3, BookId  = 2 },
            new BookReview { Title = "The Genius Behind Apple", Description = "An inspiring, raw portrait of a visionary who changed technology forever.", Rating = 5, UserId = 5, BookId  = 2 },
            new BookReview { Title = "Иновация и обсебеност", Description = "Завладяващо пътуване в живота на Стив Джобс – гениалност, примесена със сложност.", Rating = 5, UserId = 6, BookId  = 2 },
            new BookReview { Title = "Лек за съвременния хаос – Уникална визия за времето", Description = "„Времеубежище“ на Георги Господинов е изобретателна смесица от сатира, меланхолия и надежда. Книгата изследва нарастващата мания на човечеството по носталгията в епоха, когато бъдещето изглежда несигурно. Чрез „клиниките за време“ на Гаустин, които възпроизвеждат различни десетилетия, читателите са поканени да преосмислят връзката си със спомените и прогреса. Стилът на Господинов е едновременно haunting и поетичен, улавяйки крехкостта на времето и историята.", Rating = 5, UserId = 6, BookId  = 1 },
            new BookReview { Title = "A Nostalgic Time Capsule – A Brilliant Exploration of Memory", Description = "In Time Shelter, Georgi Gospodinov crafts a gripping, philosophical journey through time and memory. The novel raises profound questions: How much of our identity is tied to the past? And what happens when nostalgia turns into an escape? The protagonist, through his \"time clinics,\" helps people rediscover lost decades. But as the world falls deeper into a collective past, the story becomes eerily prophetic and unsettling. A must-read for fans of existential fiction.", Rating = 4, UserId = 3, BookId  = 1 },
            new BookReview { Title = "Любов и остроумие през вековете", Description = "Блестящ разказ за любовта, класите и независимостта. Остър ум и незабравими герои превръщат този роман в класика.", Rating = 4, UserId = 5, BookId  = 5 },
            new BookReview { Title = "A Battle of Pride and Prejudice", Description = " Elizabeth Bennet and Mr. Darcy’s story is a perfect mix of charm, wit, and societal critique, still relevant today.", Rating = 3, UserId = 6, BookId  = 5 },
            new BookReview { Title = "Любов отвъд първите впечатления", Description = "Остин улавя сложността на любовта и обществото с хумор и изящество в това непреходно произведение.", Rating = 5, UserId = 6, BookId  = 5 },
            new BookReview { Title = "Where the Magic Begins", Description = "A timeless tale of friendship, bravery, and adventure. Harry’s journey to Hogwarts is a doorway to imagination and wonder.", Rating = 5, UserId = 3, BookId  = 4 },
            new BookReview { Title = "Магическо начало", Description = "Първата стъпка в магическия свят на Дж. К. Роулинг омагьосва читатели от всички възрасти с магия, чудеса и незабравими герои.", Rating = 5, UserId = 6, BookId  = 4 },
            new BookReview { Title = "Геният на Леонардо разкрит", Description = "Завладяващ портрет на ненаситното любопитство и гения на Да Винчи. Уолтър Айзъксън майсторски изследва живота, изкуството и науката на върховния ренесансов човек.", Rating = 5, UserId = 3, BookId  = 3 },
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var bookReview in bookReviews)
            {
                await SeedBookReviewAsync(dbContext, bookReview);
            }
        }

        private static async Task SeedBookReviewAsync(ApplicationDbContext dbContext, BookReview bookReview)
        {
            var exists = await dbContext.BookReviews
                .AnyAsync(br => br.UserId == bookReview.UserId && br.BookId == bookReview.BookId);
            if (exists) return;

            await dbContext.BookReviews.AddAsync(bookReview);
            await dbContext.SaveChangesAsync();
        }
    }
}
