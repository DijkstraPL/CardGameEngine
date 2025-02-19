﻿// <auto-generated />
using System;
using CardGame_DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CardGame_DataAccess.Migrations
{
    [DbContext(typeof(CardGameDbContext))]
    partial class CardGameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CardGame_DataAccess.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("CardTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<int?>("Cooldown")
                        .HasColumnType("int");

                    b.Property<int?>("CostBlue")
                        .HasColumnType("int");

                    b.Property<int?>("CostGreen")
                        .HasColumnType("int");

                    b.Property<int?>("CostRed")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Flavour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Health")
                        .HasColumnType("int");

                    b.Property<int>("InvocationTarget")
                        .HasColumnType("int");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<string>("Rule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SetId")
                        .HasColumnType("int");

                    b.Property<int?>("SubTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Trait")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardTypeId");

                    b.HasIndex("SetId");

                    b.HasIndex("SubTypeId");

                    b.ToTable("CardGame_Cards");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.CardDeck", b =>
                {
                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("CardId", "DeckId");

                    b.HasIndex("DeckId");

                    b.ToTable("CardGame_CardDecks");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.CardRule", b =>
                {
                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("RuleId")
                        .HasColumnType("int");

                    b.HasKey("CardId", "RuleId");

                    b.HasIndex("RuleId");

                    b.ToTable("CardGame_CardRules");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.CardType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("CardGame_CardTypes");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("CardGame_Decks");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.Rule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Effect")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("CardGame_Rules");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("CardGame_Sets");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.Subtype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("CardGame_Subtypes");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.Card", b =>
                {
                    b.HasOne("CardGame_DataAccess.Entities.CardType", "CardType")
                        .WithMany()
                        .HasForeignKey("CardTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardGame_DataAccess.Entities.Set", "Set")
                        .WithMany()
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardGame_DataAccess.Entities.Subtype", "SubType")
                        .WithMany()
                        .HasForeignKey("SubTypeId");
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.CardDeck", b =>
                {
                    b.HasOne("CardGame_DataAccess.Entities.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardGame_DataAccess.Entities.Deck", "Deck")
                        .WithMany("Cards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CardGame_DataAccess.Entities.CardRule", b =>
                {
                    b.HasOne("CardGame_DataAccess.Entities.Card", "Card")
                        .WithMany("Rules")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardGame_DataAccess.Entities.Rule", "Rule")
                        .WithMany()
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
