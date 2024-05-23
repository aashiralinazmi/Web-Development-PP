namespace OOAD_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBINIT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Activity = c.String(),
                        columnName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(),
                        SingleProductPrice = c.Int(nullable: false),
                        Qunatity = c.Int(nullable: false),
                        Subtotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Customerid = c.Int(nullable: false, identity: true),
                        Customername = c.String(),
                        UserName = c.String(),
                        CustomerE_Mail = c.String(),
                        CustomerPhoneNo = c.Long(nullable: false),
                        Passward = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Customerid);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Payment = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        LoginAll_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoginAlls", t => t.LoginAll_Id)
                .Index(t => t.LoginAll_Id);
            
            CreateTable(
                "dbo.LoginAlls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Passward = c.String(),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        ProductId = c.Int(nullable: false),
                        ContactNo = c.Long(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        SingleProductPrice = c.Int(nullable: false),
                        Note = c.String(),
                        Quantity = c.Int(nullable: false),
                        Subtotal = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        invoiceid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Price = c.Int(nullable: false),
                        Category = c.String(),
                        Description = c.String(),
                        Imagepath = c.String(),
                        Stock = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(),
                        Name = c.String(),
                        Email = c.String(),
                        Review1 = c.String(),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Invoices", "LoginAll_Id", "dbo.LoginAlls");
            DropIndex("dbo.Reviews", new[] { "CustomerId" });
            DropIndex("dbo.Invoices", new[] { "LoginAll_Id" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.LoginAlls");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
            DropTable("dbo.Carts");
            DropTable("dbo.ActivityLogs");
        }
    }
}
