namespace SlimsteMens.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.GalleryQuestions",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Answer = c.String(),
                    Photo = c.Binary(),
                    Played = c.Boolean(nullable: false),
                    Gallery_Id = c.Long(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.Gallery_Id);

            CreateTable(
                "dbo.Puzzles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PuzzleQuestions",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Answer = c.String(),
                    Hint1 = c.String(),
                    Hint2 = c.String(),
                    Hint3 = c.String(),
                    Hint4 = c.String(),
                    Played = c.Boolean(nullable: false),
                    Puzzle_Id = c.Long(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Puzzles", t => t.Puzzle_Id);

            CreateTable(
                "dbo.FinaleQuestions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Question = c.String(),
                        Answer1 = c.String(),
                        Answer2 = c.String(),
                        Answer3 = c.String(),
                        Answer4 = c.String(),
                        Answer5 = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.VideoQuestions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Question = c.String(),
                        Answer1 = c.String(),
                        Answer2 = c.String(),
                        Answer3 = c.String(),
                        Answer4 = c.String(),
                        Answer5 = c.String(),
                        Video = c.String(),
                        Played = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ThreeSixNineQuestions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        Photo = c.Binary(),
                        Played = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.PuzzleQuestions", "Puzzle_Id", "dbo.Puzzles");
            DropForeignKey("dbo.GalleryQuestions", "Gallery_Id", "dbo.Galleries");
            DropTable("dbo.ThreeSixNineQuestions");
            DropTable("dbo.VideoQuestions");
            DropTable("dbo.FinaleQuestions");
            DropTable("dbo.PuzzleQuestions");
            DropTable("dbo.Puzzles");
            DropTable("dbo.GalleryQuestions");
            DropTable("dbo.Galleries");
        }
    }
}
