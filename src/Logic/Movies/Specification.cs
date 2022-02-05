using System;
using System.Linq;
using System.Linq.Expressions;

namespace Logic.Movies
{
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }


    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new IdentitySpecification<T>();

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;
            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (this == All || specification == All)
                return All;

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }


    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }


    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            BinaryExpression orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }


    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = _specification.ToExpression();
            UnaryExpression notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }

   //TODO Movie specification Class

   // inherit from Specification of Movie
   // Generate code to overrid the ToExpression abstract method
   // return expression to determine the specification (here: suitable for children)

    public sealed class MovieForKidsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.MpaaRating <= MpaaRating.PG;
        }
    }


    public sealed class AvailableOnCDSpecification : Specification<Movie>
    {
        private const int MonthsBeforeDVDIsOut = 6;

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.ReleaseDate <= DateTime.Now.AddMonths(-MonthsBeforeDVDIsOut);
        }
    }


    public sealed class MovieDirectedBySpecification : Specification<Movie>
    {
        private readonly string _director;

        public MovieDirectedBySpecification(string director)
        {
            _director = director;
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Director.Name == _director;
        }
    }

    #region Specification Class notes
    //TODO Specification Class
    //public abstract class Specification<T>
    //{
    //   /// <summary>
    //   /// IsSatsifiedBy Method
    //   /// performs in-memory validation on single objects
    //   /// Abstract & will be provided by classes inheriting from specification
    //   /// then needs the underlying expression itself in order to use the repository
    //   /// same trick as generic specification
    //   /// we compile the expression into a delegate and rename it to predicate (predicate: function that accepts object & return a boolean
    //   /// </summary>
    //   /// <param name="entity"></param>
    //   /// <returns></returns>
    //   public bool IsSatisfiedBy(T entity)
    //   {
    //      // compile provided expression into delegate / rename it to delegate
    //      // Func<T, bool> delegate = ToExpression().Compile();
    //      Func<T, bool> predicate = ToExpression().Compile();
    //      // Call that predicate & pass the entity to it as an input param
    //      return predicate(entity);

    //      // Note Generic Specification Class, similar to new abstract class  
    //   }
    //   // **  V The expression required from the inherited classes  V  **
    //   // NOTE 2:  ToExpression() instead of using an Expression prop - indicates that although we use C# heavily use C# expressions to implement the functionality, they are second class citizen here
    //   // Expression is just something we need to convert our specification to
    //   public abstract Expression<Func<T, bool>> ToExpression();
    //}
    #endregion
}
