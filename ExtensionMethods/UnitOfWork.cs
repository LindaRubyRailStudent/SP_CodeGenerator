

// This is the output code from your template
// you only get syntax-highlighting here - not intellisense
namespace ExtensionMethods{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Data;
	using DataBaseLayer;

  public class UnitOfWork : IDisposable{

	private AWorksLTEntities context = new AWorksLTEntities();
	private GenericRepository<Product> productRepository;


	public GenericRepository<Product> ProductRepository
     {
         get
         {
             if (this.productRepository == null)
             {
                 this.productRepository = new GenericRepository<Product>(context);
             }
             return productRepository;
         }
     }
 

	public void Save()
        {
            context.SaveChanges();
        }

	private bool disposed = false;

    protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

	public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
  }
}
 
