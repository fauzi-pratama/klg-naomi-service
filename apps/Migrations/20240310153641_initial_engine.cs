using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apps.Migrations
{
    /// <inheritdoc />
    public partial class initial_engine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "engine_workflow",
                columns: table => new
                {
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_workflow", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "engine_rule",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    engine_workflow_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    redeem_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cls = table.Column<int>(type: "integer", nullable: false),
                    lvl = table.Column<int>(type: "integer", nullable: false),
                    item_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    result_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    action_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    action_value = table.Column<string>(type: "text", nullable: true),
                    max_value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    max_multiple = table.Column<int>(type: "integer", nullable: true),
                    max_use = table.Column<int>(type: "integer", nullable: true),
                    max_balance = table.Column<decimal>(type: "numeric", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ref_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    multiple_qty = table.Column<int>(type: "integer", nullable: false),
                    max_qty = table.Column<int>(type: "integer", nullable: false),
                    entertaiment_nip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    terms_condition = table.Column<string>(type: "text", nullable: true),
                    show_status = table.Column<bool>(type: "boolean", nullable: false),
                    member_status = table.Column<bool>(type: "boolean", nullable: false),
                    member_new_status = table.Column<bool>(type: "boolean", nullable: false),
                    member_quota_periode = table.Column<int>(type: "integer", nullable: false),
                    member_repeat_periode = table.Column<int>(type: "integer", nullable: false),
                    image_link = table.Column<string>(type: "text", nullable: true),
                    absolute_status = table.Column<bool>(name: "absolute_status ", type: "boolean", nullable: false),
                    voucherStatus = table.Column<bool>(name: "voucherStatus ", type: "boolean", nullable: false),
                    voucher_code = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_rule", x => x.code);
                    table.ForeignKey(
                        name: "FK_engine_rule_engine_workflow_engine_workflow_code",
                        column: x => x.engine_workflow_code,
                        principalTable: "engine_workflow",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "engine_workflow_expression",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_workflow_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    expression = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_workflow_expression", x => x.Id);
                    table.ForeignKey(
                        name: "FK_engine_workflow_expression_engine_workflow_engine_workflow_~",
                        column: x => x.engine_workflow_code,
                        principalTable: "engine_workflow",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "engine_rule_apps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_rule_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_rule_apps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_engine_rule_apps_engine_rule_engine_rule_code",
                        column: x => x.engine_rule_code,
                        principalTable: "engine_rule",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "engine_rule_expression",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_rule_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "text", nullable: false),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    group_line = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    expression = table.Column<string>(type: "text", nullable: false),
                    link = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_rule_expression", x => x.Id);
                    table.ForeignKey(
                        name: "FK_engine_rule_expression_engine_rule_engine_rule_code",
                        column: x => x.engine_rule_code,
                        principalTable: "engine_rule",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "engine_rule_membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_rule_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_rule_membership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_engine_rule_membership_engine_rule_engine_rule_code",
                        column: x => x.engine_rule_code,
                        principalTable: "engine_rule",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "engine_rule_mop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_rule_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    selection_code = table.Column<string>(type: "text", nullable: false),
                    selection_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    group_code = table.Column<string>(type: "text", nullable: false),
                    group_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_rule_mop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_engine_rule_mop_engine_rule_engine_rule_code",
                        column: x => x.engine_rule_code,
                        principalTable: "engine_rule",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "engine_rule_result",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_rule_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    group_line = table.Column<int>(type: "integer", nullable: false),
                    item = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    max_value = table.Column<string>(type: "text", nullable: false),
                    link = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_rule_result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_engine_rule_result_engine_rule_engine_rule_code",
                        column: x => x.engine_rule_code,
                        principalTable: "engine_rule",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "engine_rule_site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_rule_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_by = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_rule_site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_engine_rule_site_engine_rule_engine_rule_code",
                        column: x => x.engine_rule_code,
                        principalTable: "engine_rule",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "engine_rule_variable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    engine_rule_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    line_num = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    expression = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_engine_rule_variable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_engine_rule_variable_engine_rule_engine_rule_code",
                        column: x => x.engine_rule_code,
                        principalTable: "engine_rule",
                        principalColumn: "code");
                });

            migrationBuilder.CreateIndex(
                name: "IX_engine_rule_engine_workflow_code",
                table: "engine_rule",
                column: "engine_workflow_code");

            migrationBuilder.CreateIndex(
                name: "IX_engine_rule_apps_engine_rule_code",
                table: "engine_rule_apps",
                column: "engine_rule_code");

            migrationBuilder.CreateIndex(
                name: "IX_engine_rule_expression_engine_rule_code",
                table: "engine_rule_expression",
                column: "engine_rule_code");

            migrationBuilder.CreateIndex(
                name: "IX_engine_rule_membership_engine_rule_code",
                table: "engine_rule_membership",
                column: "engine_rule_code");

            migrationBuilder.CreateIndex(
                name: "IX_engine_rule_mop_engine_rule_code",
                table: "engine_rule_mop",
                column: "engine_rule_code");

            migrationBuilder.CreateIndex(
                name: "IX_engine_rule_result_engine_rule_code",
                table: "engine_rule_result",
                column: "engine_rule_code");

            migrationBuilder.CreateIndex(
                name: "IX_engine_rule_site_engine_rule_code",
                table: "engine_rule_site",
                column: "engine_rule_code");

            migrationBuilder.CreateIndex(
                name: "IX_engine_rule_variable_engine_rule_code",
                table: "engine_rule_variable",
                column: "engine_rule_code");

            migrationBuilder.CreateIndex(
                name: "IX_engine_workflow_expression_engine_workflow_code",
                table: "engine_workflow_expression",
                column: "engine_workflow_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "engine_rule_apps");

            migrationBuilder.DropTable(
                name: "engine_rule_expression");

            migrationBuilder.DropTable(
                name: "engine_rule_membership");

            migrationBuilder.DropTable(
                name: "engine_rule_mop");

            migrationBuilder.DropTable(
                name: "engine_rule_result");

            migrationBuilder.DropTable(
                name: "engine_rule_site");

            migrationBuilder.DropTable(
                name: "engine_rule_variable");

            migrationBuilder.DropTable(
                name: "engine_workflow_expression");

            migrationBuilder.DropTable(
                name: "engine_rule");

            migrationBuilder.DropTable(
                name: "engine_workflow");
        }
    }
}
