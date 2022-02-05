using System;
using System.Linq.Expressions;

namespace Logic.Movies
{
   //TODO GenericSpecification Class
   public class GenericSpecification<T>
   {
      public Expression<Func<T, bool>> Expression { get; }

      // ** Requires expression to be provided by inheriting class (cannot be from outside world) 
      //** means domain knowledge will be encapsulated in those inheriting classes & not provided by client code
      public GenericSpecification(Expression<Func<T, bool>> expression)
      {
         Expression = expression;
      }


      public bool IsSatisfiedBy(T entity)
      {
         return Expression.Compile().Invoke(entity);
      }
   }


}
