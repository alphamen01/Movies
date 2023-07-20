using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies
{
	 class Program
	{
		static void Main(string[] args)
		{
			var db = new MoviesDbContext();
			ShowMenu(db);

			System.Console.WriteLine("Presione cualquier tecla continuar");
			Console.ReadLine();
		}

		private static void Show(MoviesDbContext db)
		{
			System.Console.WriteLine();
			var all = db.Movies.ToListAsync().Result;

            if (all!=null)
            {
                foreach (var movie in all)
                {
					System.Console.WriteLine(movie.Name);
                }

            }
			ShowMenu(db);
        }

		private static void ShowMenu(MoviesDbContext db)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			System.Console.WriteLine();
			System.Console.WriteLine("Seleccione la opcion deseada");
			System.Console.WriteLine("1.- Consultar las peliculas");
			System.Console.WriteLine("2.- Crear una nueva pelicula");
			System.Console.WriteLine();

			var option = Console.ReadLine();

			Console.ForegroundColor = ConsoleColor.Gray;
						
            if (option == "1")
            {
				Show(db);
            }
            else
            {
				Create(db);
            }
        }

		

		private static void Create(MoviesDbContext db)
		{
			System.Console.WriteLine();
			System.Console.WriteLine("Escriba el nombre de la pelicula:");
			var name = Console.ReadLine();
			System.Console.WriteLine();
			System.Console.WriteLine("Escriba el año de estreno:");
			var year = Console.ReadLine();

			var newMovie = new Movie();
			newMovie.Name = name;
			newMovie.Year = int.Parse(year);

			
			db.Movies.Add(newMovie);

			var result = db.SaveChanges();
			if (result == 1)
			{
				System.Console.WriteLine();
				System.Console.WriteLine("La pelicula se guardo correctamente");
			}
			else
			{
				System.Console.WriteLine("Error");
			}

			ShowMenu(db);
		}
	}

	class Movie
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(500)]
		public string Name { get; set; }

		[Required]
		[Range(1900,2100)]
		public int Year { get; set; }

	}

    class MoviesDbContext: DbContext
    {
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source=PROYCANVSIN19;Initial Catalog=Movies;User ID=sa;Password=Lesg2022;");
		}

		
		public DbSet<Movie> Movies { get; set; }
        
    }
}
