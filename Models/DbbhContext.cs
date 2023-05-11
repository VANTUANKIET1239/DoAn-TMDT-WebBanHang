using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CloudComputing.Models;

public partial class DbbhContext : DbContext
{
    public DbbhContext()
    {
    }

    public DbbhContext(DbContextOptions<DbbhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BanPhim> BanPhims { get; set; }

    public virtual DbSet<ChuotMayTinh> ChuotMayTinhs { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DetailCart> DetailCarts { get; set; }

    public virtual DbSet<DetailHoaDon> DetailHoaDons { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<PcManHinh> PcManHinhs { get; set; }

    public virtual DbSet<PcMayTinhBo> PcMayTinhBos { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TaiNghe> TaiNghes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-SSK4EOU4;Database=DBBH;Trusted_Connection=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BanPhim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BanPhim");

            entity.ToTable("Ban_phim");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Den).HasMaxLength(20);
            entity.Property(e => e.IdDanhmuc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_DANHMUC");
            entity.Property(e => e.KetNoi)
                .HasMaxLength(50)
                .HasColumnName("Ket_noi");
            entity.Property(e => e.KichThuoc)
                .HasMaxLength(50)
                .HasColumnName("Kich_thuoc");
            entity.Property(e => e.LoaiBanPhim)
                .HasMaxLength(30)
                .HasColumnName("Loai_ban_phim");
            entity.Property(e => e.Mau).HasMaxLength(10);
        });

        modelBuilder.Entity<ChuotMayTinh>(entity =>
        {
            entity.ToTable("Chuot_may_tinh");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.DenLed)
                .HasMaxLength(10)
                .HasColumnName("Den_led");
            entity.Property(e => e.IdDanhmuc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_DANHMUC");
            entity.Property(e => e.KetNoi)
                .HasMaxLength(30)
                .HasColumnName("Ket_noi");
            entity.Property(e => e.KichThuoc)
                .HasMaxLength(50)
                .HasColumnName("Kich_thuoc");
            entity.Property(e => e.KieuKetNoi)
                .HasMaxLength(20)
                .HasColumnName("Kieu_ket_noi");
            entity.Property(e => e.Mau).HasMaxLength(15);
            entity.Property(e => e.SoNutBam).HasColumnName("So_nut_bam");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.ToTable("Danh_muc");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.TenBang)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.TenDm)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("Ten_DM");
        });

        modelBuilder.Entity<DetailCart>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdSp }).HasName("PK_Detail_cart");

            entity.ToTable("Detail_Cart");

            entity.Property(e => e.IdUser)
                .HasMaxLength(10)
                .HasColumnName("ID_User");
            entity.Property(e => e.IdSp)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID_SP");
            entity.Property(e => e.SoLuong).HasColumnName("So_luong");
        });

        modelBuilder.Entity<DetailHoaDon>(entity =>
        {
            entity.HasKey(e => e.IdHoaDon);

            entity.ToTable("Detail_Hoa_don");

            entity.Property(e => e.IdHoaDon)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_Hoa_don");
            entity.Property(e => e.DonGia).HasColumnName("Don_gia");
            entity.Property(e => e.IdSp)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_SP");
            entity.Property(e => e.SoLuong).HasColumnName("So_luong");
            entity.Property(e => e.TenSp)
                .HasMaxLength(50)
                .HasColumnName("Ten_SP");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.ToTable("Hoa_don");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(70)
                .HasColumnName("Dia_chi");
            entity.Property(e => e.IdUser)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_User");
            entity.Property(e => e.Payment).HasMaxLength(20);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.ShipFee).HasColumnName("Ship_fee");
            entity.Property(e => e.TimeStamp)
                .HasColumnType("datetime")
                .HasColumnName("Time_stamp");
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LapTop");

            entity.ToTable("Laptop");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.BanPhim)
                .HasMaxLength(50)
                .HasColumnName("Ban_phim");
            entity.Property(e => e.ChipDoHoa)
                .HasMaxLength(70)
                .HasColumnName("Chip_do_hoa");
            entity.Property(e => e.CongKetNoi)
                .HasMaxLength(100)
                .HasColumnName("Cong_ket_noi");
            entity.Property(e => e.Cpu)
                .HasMaxLength(100)
                .HasColumnName("CPU");
            entity.Property(e => e.HeDieuHanh)
                .HasMaxLength(50)
                .HasColumnName("He_dieu_hanh");
            entity.Property(e => e.IdDanhmuc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_DANHMUC");
            entity.Property(e => e.LuuTru)
                .HasMaxLength(50)
                .HasColumnName("Luu_tru");
            entity.Property(e => e.ManHinh)
                .HasMaxLength(70)
                .HasColumnName("Man_hinh");
            entity.Property(e => e.MauSac)
                .HasMaxLength(50)
                .HasColumnName("Mau_sac");
            entity.Property(e => e.Pin).HasMaxLength(50);
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .HasColumnName("RAM");
            entity.Property(e => e.SeriesLaptop)
                .HasMaxLength(50)
                .HasColumnName("Series_laptop");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.ToTable("Nguoi_Dung");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(100)
                .HasColumnName("Dia_chi");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.PassWord)
                .HasMaxLength(300)
                .HasColumnName("Pass_word");
            entity.Property(e => e.Roles).HasMaxLength(20);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(50);
        });

        modelBuilder.Entity<PcManHinh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PC_man_hinh");

            entity.ToTable("PC_Man_hinh");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.BeMat)
                .HasMaxLength(30)
                .HasColumnName("Be_mat");
            entity.Property(e => e.CongXuatHinh)
                .HasMaxLength(60)
                .HasColumnName("Cong_xuat_hinh");
            entity.Property(e => e.DoPhanGiai)
                .HasMaxLength(30)
                .HasColumnName("Do_phan_giai");
            entity.Property(e => e.GocNhin)
                .HasMaxLength(30)
                .HasColumnName("Goc_nhin");
            entity.Property(e => e.IdDanhmuc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_DANHMUC");
            entity.Property(e => e.KichThuoc)
                .HasMaxLength(50)
                .HasColumnName("Kich_thuoc");
            entity.Property(e => e.KieuManHinh)
                .HasMaxLength(20)
                .HasColumnName("Kieu_man_hinh");
            entity.Property(e => e.TamNen)
                .HasMaxLength(15)
                .HasColumnName("Tam_nen");
            entity.Property(e => e.TanSoQuet)
                .HasMaxLength(10)
                .HasColumnName("Tan_so_quet");
        });

        modelBuilder.Entity<PcMayTinhBo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PC_MayTinhBo");

            entity.ToTable("PC_May_tinh_bo");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.CongKetNoi)
                .HasMaxLength(100)
                .HasColumnName("Cong_ket_noi");
            entity.Property(e => e.Cpu)
                .HasMaxLength(100)
                .HasColumnName("CPU");
            entity.Property(e => e.HeDieuHanh)
                .HasMaxLength(50)
                .HasColumnName("He_dieu_hanh");
            entity.Property(e => e.IdDanhmuc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_DANHMUC");
            entity.Property(e => e.LuuTru)
                .HasMaxLength(50)
                .HasColumnName("Luu_tru");
            entity.Property(e => e.MauSac)
                .HasMaxLength(15)
                .HasColumnName("Mau_sac");
            entity.Property(e => e.Ram)
                .HasMaxLength(60)
                .HasColumnName("RAM");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.IdSp).HasName("PK_San_Pham");

            entity.ToTable("San_pham");

            entity.Property(e => e.IdSp)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID_SP");
            entity.Property(e => e.BaoHanh)
                .HasMaxLength(10)
                .HasColumnName("BAO_HANH");
            entity.Property(e => e.Gia).HasColumnName("GIA");
            entity.Property(e => e.HinhAnh).HasColumnName("HINH_ANH");
            entity.Property(e => e.IdDm)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID_DM");
            entity.Property(e => e.KhoiLuong)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("KHOI_LUONG");
            entity.Property(e => e.TenSanPham)
                .HasMaxLength(150)
                .HasColumnName("TEN_SAN_PHAM");
            entity.Property(e => e.ThuongHieu)
                .HasMaxLength(20)
                .HasColumnName("THUONG_HIEU");
            entity.Property(e => e.TrangThai).HasColumnName("TRANG_THAI");
        });

        modelBuilder.Entity<TaiNghe>(entity =>
        {
            entity.ToTable("Tai_nghe");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.IdDanhmuc)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("ID_DANHMUC");
            entity.Property(e => e.KetNoi)
                .HasMaxLength(20)
                .HasColumnName("Ket_noi");
            entity.Property(e => e.Kieu).HasMaxLength(25);
            entity.Property(e => e.Mau).HasMaxLength(15);
            entity.Property(e => e.Microphone).HasMaxLength(5);
            entity.Property(e => e.TanSoPhanHoi)
                .HasMaxLength(25)
                .HasColumnName("Tan_so_phan_hoi");
            entity.Property(e => e.TroKhang)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Tro_khang");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
