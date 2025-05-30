﻿using Library.Responses;
using System.Linq.Expressions;

namespace Library.Interface
{
	public interface IGenericInterface<T> where T:class
	{
		Task<Response> CreateAsync(T entity);
		Task<Response> UpdateAsync(T entity);
		Task<Response> DeleteAsync(T entity); 
		Task<IEnumerable<T>> GetListAsync(T entity);
		Task<T> GetByIdAsync(int id);

		Task<T> GetByAsync(Expression<Func<T, bool>> predicate); 
	}
}
