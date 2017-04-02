using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ask.about.Models;

namespace Ask.about.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20170402110933_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ask.about.Models.Comment", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<DateTime>("DateId");

                    b.Property<int?>("QuestionId");

                    b.Property<int>("Rating");

                    b.Property<int?>("ReplyQuestionId");

                    b.Property<int?>("ReplyUserId");

                    b.Property<string>("Text");

                    b.HasKey("UserId", "DateId");

                    b.HasAlternateKey("DateId", "UserId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("ReplyUserId", "ReplyQuestionId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Ask.about.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("Likes");

                    b.Property<short>("RepliesNumber");

                    b.Property<string>("Text");

                    b.Property<int?>("TopicId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Ask.about.Models.Reply", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("QuestionId");

                    b.Property<int>("CommentariesNumber");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Rating");

                    b.Property<string>("Text");

                    b.HasKey("UserId", "QuestionId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("Ask.about.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuestionsNumber");

                    b.Property<int>("Rating");

                    b.Property<int>("RepliesNumber");

                    b.Property<string>("Title");

                    b.Property<int>("UsersCount");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Ask.about.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ask.about.Models.Comment", b =>
                {
                    b.HasOne("Ask.about.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("Ask.about.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ask.about.Models.Reply")
                        .WithMany("Comments")
                        .HasForeignKey("ReplyUserId", "ReplyQuestionId");
                });

            modelBuilder.Entity("Ask.about.Models.Question", b =>
                {
                    b.HasOne("Ask.about.Models.Topic", "Topic")
                        .WithMany("Questions")
                        .HasForeignKey("TopicId");

                    b.HasOne("Ask.about.Models.User", "User")
                        .WithMany("Questions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ask.about.Models.Reply", b =>
                {
                    b.HasOne("Ask.about.Models.User", "User")
                        .WithMany("Replies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
