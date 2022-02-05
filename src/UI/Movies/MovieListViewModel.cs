using System.Collections.Generic;
using System.Windows;
using CSharpFunctionalExtensions;
using Logic.Movies;
using Logic.Utils;

using UI.Common;

namespace UI.Movies
{
    public class MovieListViewModel : ViewModel
    {
        private readonly MovieRepository _repository;

        public Command SearchCommand { get; }
        public Command<long> BuyAdultTicketCommand { get; }
        public Command<long> BuyChildTicketCommand { get; }
        public Command<long> BuyCDCommand { get; }
        public IReadOnlyList<MovieDto> Movies { get; private set; }

        public bool ForKidsOnly { get; set; }
        public double MinimumRating { get; set; }
        public bool OnCD { get; set; }

        public MovieListViewModel()
        {
            _repository = new MovieRepository();

            SearchCommand = new Command(Search);
            BuyAdultTicketCommand = new Command<long>(BuyAdultTicket);
            BuyChildTicketCommand = new Command<long>(BuyChildTicket);
            BuyCDCommand = new Command<long>(BuyCD);
        }

        private void BuyCD(long movieId)
        {
            Maybe<Movie> movieOrNothing = _repository.GetOne(movieId);
            if (movieOrNothing.HasNoValue)
                return;

            Movie movie = movieOrNothing.Value;
            var spec = new AvailableOnCDSpecification();
            if (!spec.IsSatisfiedBy(movie))
            {
                MessageBox.Show("The movie doesn't have a CD version", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("You've bought a ticket", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BuyChildTicket(long movieId)
        {
            Maybe<Movie> movieOrNothing = _repository.GetOne(movieId);
            if (movieOrNothing.HasNoValue)
                return;

            /// TODO Use strongly typed specifications for In-Memory validation as well as database searching
            Movie movie = movieOrNothing.Value;
            var spec = new MovieForKidsSpecification();
            if (!spec.IsSatisfiedBy(movie))
            {
                MessageBox.Show("The movie is not suitable for children", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("You've bought a ticket", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BuyAdultTicket(long movieId)
        {
            MessageBox.Show("You've bought a ticket", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Search()
        {
         // TODO Can use Specification in our search method
         /// Use specification in a method
         /// 1. Instantiate the desired specification
         /// 2. Can pass it to the repository
         /// 
         ///    Specification<Movie> spec = Specification<Movie>.All;
         ///    Movies = _repository.GetList(spec);
         ///    Notify(nameof(Movies));
         /// 
         Specification<Movie> spec = Specification<Movie>.All;

            if (ForKidsOnly)
            {
                spec = spec.And(new MovieForKidsSpecification());
            }
            if (OnCD)
            {
                spec = spec.And(new AvailableOnCDSpecification());
            }

            Movies = _repository.GetList(spec, MinimumRating);

            Notify(nameof(Movies));
        }

      ///  //TODO Using Specification for Database Searching
      /// Repository Class - use Specification
      /// TODO Use specification in method in repository
      ///      need to use specification instead of GenericSpecification
      ///      & the method ToExpression instead of Expression Property
      //   public IReadOnlyList<MovieDto> GetList(
         //   Specification<Movie> specification)
         //{
         //   using (ISession session = SessionFactory.OpenSession())
         //   {
         //      return session.Query<Movie>()
         //         .Where(specification.ToExpression())
         //         // other logic here
         //         .ToList();
         //   }
         //}
   }
}
