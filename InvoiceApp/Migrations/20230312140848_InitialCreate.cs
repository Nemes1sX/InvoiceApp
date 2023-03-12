using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATPrecent = table.Column<int>(type: "int", nullable: false),
                    EuropeanUnion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Individuals_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATPayer = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalPersons_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    LegalPersonId = table.Column<int>(type: "int", nullable: false),
                    PayerLegalLegalPersonId = table.Column<int>(type: "int", nullable: true),
                    PayerIndividualId = table.Column<int>(type: "int", nullable: true),
                    PayerLegalPersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Individuals_PayerIndividualId",
                        column: x => x.PayerIndividualId,
                        principalTable: "Individuals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_LegalPersons_LegalPersonId",
                        column: x => x.LegalPersonId,
                        principalTable: "LegalPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_LegalPersons_PayerLegalPersonId",
                        column: x => x.PayerLegalPersonId,
                        principalTable: "LegalPersons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasePrice = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PriceWithVAT = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_CountryId",
                table: "Individuals",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalPersons_CountryId",
                table: "LegalPersons",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LegalPersonId",
                table: "Orders",
                column: "LegalPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PayerIndividualId",
                table: "Orders",
                column: "PayerIndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PayerLegalPersonId",
                table: "Orders",
                column: "PayerLegalPersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "LegalPersons");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
