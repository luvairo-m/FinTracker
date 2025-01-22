using FluentMigrator;

namespace FinTracker.Dal.Migrations;

[Migration(202501221907)]
public class PaymentAmountToMoney_202501221907 : Migration
{
    public override void Up()
    {
        Alter
            .Table("Payment").InSchema("dbo")
            .AlterColumn("Amount").AsCurrency();
    }

    public override void Down()
    {
    }
}