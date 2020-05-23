using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace homework1
{
    public partial class ContosoUniversityContext : DbContext
    {
        
        public override int SaveChanges()
        {
            this.ChangeTracker.Entries();
            var entlties = this.ChangeTracker.Entries();

            foreach(var entry in entlties)
            {
                var test = entry.Entity.GetType().FullName;
                // Console.WriteLine("EntityName:{O}", entry.Entity.GetType().FullName);
                // Console.WriteLine("Status:{O}", entry.State);

                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues.SetValues(new { IsDeleted = true });
                }

                if(entry.State == EntityState.Modified)
                {
                    entry.CurrentValues.SetValues(new{ModifiedOn = DateTime.UtcNow});

                }

            }
            
            return base.SaveChanges();

        }

    }
}
