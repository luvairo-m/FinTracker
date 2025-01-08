using System.Data;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace FinTracker.Dal.Migrations;

[Migration(202501081823)]
public class Initial_202501081823 : Migration
{
    public override void Up()
    {
        Create
            .Table("Category").InSchema("dbo")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Title").AsString(128).Unique().NotNullable()
            .WithColumn("Description").AsString(1024).Nullable();

        Create
            .Table("Currency").InSchema("dbo")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Title").AsString(64).Unique().NotNullable()
            .WithColumn("Sign").AsString(6).NotNullable();
        
        Create
            .Table("Bill").InSchema("dbo")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Title").AsString(128).Unique().NotNullable()
            .WithColumn("Balance").AsCurrency().NotNullable()
            .WithColumn("Description").AsString(1024).Nullable()
            .WithColumn("CurrencyId").AsGuid().ForeignKey("Currency", "Id").OnDelete(Rule.SetNull).Nullable();

        Create
            .Table("Payment").InSchema("dbo")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Title").AsString(128).NotNullable()
            .WithColumn("Description").AsString(1024).Nullable()
            .WithColumn("Amount").AsCurrency().NotNullable()
            .WithColumn("Type").AsInt16().NotNullable()
            .WithColumn("Date").AsDate().NotNullable()
            .WithColumn("BillId").AsGuid().ForeignKey("Bill", "Id").OnDelete(Rule.SetNull).Nullable();

        Create
            .Table("PaymentCategory").InSchema("dbo")
            .WithColumn("PaymentId").AsGuid().NotNullable()
            .WithColumn("CategoryId").AsGuid().NotNullable();

        Create
            .PrimaryKey()
            .OnTable("PaymentCategory")
            .Columns("PaymentId", "CategoryId");

        var rubleId = Guid.NewGuid();
        
        Insert
            .IntoTable("Category").InSchema("dbo")
            .Row(new { Id = Guid.NewGuid(), Title = "Еда", Description = "Расходы на продукты" })
            .Row(new { Id = Guid.NewGuid(), Title = "Развлечения" })
            .Row(new { Id = Guid.NewGuid(), Title = "Учеба" });

        Insert
            .IntoTable("Currency").InSchema("dbo")
            .Row(new { Id = Guid.NewGuid(), Title = "Американский доллар", Sign = "$" })
            .Row(new { Id = rubleId, Title = "Российский рубль", Sign = "₽" });

        Insert
            .IntoTable("Bill").InSchema("dbo")
            .Row(new { Id = Guid.NewGuid(), Title = "Т-Банк", Balance = 15_000, CurrencyId = rubleId });
    }

    public override void Down()
    {
        Delete.Table("PaymentCategory").IfExists().InSchema("dbo");
        Delete.Table("Category").IfExists().InSchema("dbo");
        Delete.Table("Payment").IfExists().InSchema("dbo");
        Delete.Table("Bill").IfExists().InSchema("dbo");
        Delete.Table("Currency").IfExists().InSchema("dbo");
    }
}