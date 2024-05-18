using System;
using System.Collections;
using System.Collections.Generic;

internal class Program
{
    static MachinesManager machinesManager = new MachinesManager();
    static void Main()
    {
        while (true)
        {
            int operation = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Makine ekleme ---> 1    Makine Silme ---> 2    Tüm makineler ---> 3\nProgram ekleme ---> 4    Program Silme ---> 5    Tüm programlar ---> 6\nProgram Başlatma ---> 7    Program Bitirme ---> 8    Çalışan makineler ---> 9\nUygulamayı kapatmak ---> 0\n-----------------------------------------------------------------------------\nYapmak istediğiniz işlemi seçiniz: ");
                try { operation = int.Parse(Console.ReadLine()); break; }
                catch (Exception e) { Console.WriteLine(e); Console.ReadKey(); }
            }
            if (operation == 1)
            {
                machinesManager.AddNewMachine();
            }
            else if (operation == 2)
            {
                machinesManager.DeleteMachine();
            }
            else if (operation == 3)
            {
                machinesManager.FindAllMachines();
            }
            else if (operation == 4)
            {
                machinesManager.AddNewProgram();
            }
            else if (operation == 5)
            {
                machinesManager.DeleteProgram();
            }
            else if (operation == 6)
            {
                machinesManager.FindAllPrograms();
            }
            else if (operation == 7)
            {
                machinesManager.StartProgram();
            }
            else if (operation == 8)
            {
                machinesManager.FinishProgram();
            }
            else if (operation == 9)
            {
                machinesManager.FindAllWorkingMachines();
            }
            else if (operation == 0)
            {
                Console.WriteLine("Uygulama kapatılsın mı?\nEvet ---> 1    Hayır ---> '1 hariç herhangi bir tuşa basınız!'");
                char key = Console.ReadKey().KeyChar;
                if (key == '1')
                {
                    Console.WriteLine("\nUygulama kapatıldı!");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}

class MachineProgram
{
    /// <summary>
    /// Program numarası
    /// </summary>
    internal int programID { get; set; }
    /// <summary>
    /// Program ismi
    /// </summary>
    internal string programName { get; set; }
    /// <summary>
    /// Dakika cinsinden çalışma saati
    /// </summary>
    internal int programTime { get; set; }

    public MachineProgram(int programID, string programName, int programTime)
    {
        this.programID = programID;
        this.programName = programName;
        this.programTime = programTime;
    }
}

class Machine
{
    /// <summary>
    /// Makine numarası
    /// </summary>
    internal int machineID { get; set; }
    /// <summary>
    /// Makinenin programları: süresi, program adı, program numarası
    /// </summary>
    internal List<MachineProgram> machinePrograms { get; set; }
    /// <summary>
    /// Makinenin durumu: bozuk, boş, dolu
    /// </summary>
    internal int machineCase { get; set; }
    /// <summary>
    /// İlk kullanım tarihi
    /// </summary>
    internal DateTime machineAddBeginDate { get; set; }

    public Machine(int machineID, List<MachineProgram> machinePrograms, int machineCase)
    {
        this.machineID = machineID;
        this.machinePrograms = machinePrograms;
        this.machineCase = machineCase;
        this.machineAddBeginDate = DateTime.Now;
    }
} 

class WorkingCase
{
    internal int machineID { get; set; }

    internal int programID { get; set; }

    internal DateTime beginTime { get; set; }

    public WorkingCase (int machineID, int programID, DateTime beginTime)
    {
        this.machineID = machineID;
        this.programID = programID;
        this.beginTime = beginTime;
    }
}

class MachinesManager
{
    /// <summary>
    /// Makineleri tutar
    /// </summary>
    List<Machine> machines;
    /// <summary>
    /// Programları tutar
    /// </summary>
    List<MachineProgram> machinePrograms;
    /// <summary>
    /// Çalışan makinenin programı hakkında bilgiler
    /// </summary>
    List<WorkingCase> workingCases;

    public MachinesManager() 
    {
        machines = new List<Machine>();
        machinePrograms = new List<MachineProgram>();
        workingCases = new List<WorkingCase>();
    }

    /// <summary>
    /// Yeni Makine Ekleme
    /// </summary>
    /// <param name="machine"></param>
    public void AddNewMachine()
    {
        int machineID, machineCase;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Makine Ekleme     >>>>>\n---------------------------------------");
            Console.Write("Makinenin numarasını giriniz: ");
            try { machineID = int.Parse(Console.ReadLine()); if (machineID > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Makine Ekleme     >>>>>\n---------------------------------------");
            Console.Write("Bozuk ---> 0     Dolu ---> 1     Boş ---> 2\nMakinenin durumunu giriniz: ");
            try { machineCase = int.Parse(Console.ReadLine()); if (machineCase >= 0 && machineCase <= 2) break; else Console.WriteLine("Hatalı giriş tekrardan 0-2 arası rakam giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        machines.Add(new Machine(machineID, machinePrograms, machineCase));
        Console.WriteLine("Yeni makine başarıyla eklendi.\nDevam etmek için klavyeden bir tuşa basınız!");
        Console.ReadKey();
    }

    public void DeleteMachine()
    {
        int machineID;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Makine Silme     >>>>>\n---------------------------------------");
            Console.Write("Makinenin numarasını giriniz: ");
            try { machineID = int.Parse(Console.ReadLine()); if (machineID > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        for (int i = 0; i < machines.Count; i++)
        {
            if (machines[i].machineID == machineID)
            {
                machines.Remove(machines[i]);
                Console.WriteLine("Makine başarıyla silindi.\nDevam etmek için klavyeden bir tuşa basınız!");
                Console.ReadKey();
                break;
            }
            else if (i == machines.Count - 1)
            {
                Console.WriteLine("Böyle bir makine bulunamadı!");
                Console.ReadKey();
            }
        }
    }

    public void FindAllMachines()
    {
        Console.Clear();
        Console.WriteLine("<<<<<    Tüm Makineler     >>>>>\n---------------------------------------");
        if (machines.Count > 0)
        {
            for (int i = 0; i < machines.Count; i++)
            {
                string durum = null;
                if (machines[i].machineCase == 0)
                    durum = "Bozuk";
                else if (machines[i].machineCase == 1)
                    durum = "Dolu";
                else if (machines[i].machineCase == 2)
                    durum = "Boş";
                Console.WriteLine("Makine Numarası: " + machines[i].machineID + "   Makine Durumu: " + durum + "    Makine Kayıt Zamanı: " + machines[i].machineAddBeginDate);
            }
        }
        else
        {
            Console.WriteLine("Kayıtlı hiçbir makine bulunamadı!");
        }
        Console.WriteLine("Devam etmek için klavyeden bir tuşa basınız!");
        Console.ReadKey();
    }
    public void AddNewProgram()
    {
        int programID, programTime;
        string programName = string.Empty;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Program Ekleme     >>>>>\n---------------------------------------");
            Console.Write("Programın numarasını giriniz: ");
            try { programID = int.Parse(Console.ReadLine()); if (programID > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Program Ekleme     >>>>>\n---------------------------------------");
            Console.Write("Programın ismini giriniz: ");
            try { programName = Console.ReadLine(); if (programName != string.Empty) break; else Console.WriteLine("Hatalı giriş, lütfen program ismini giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Program Ekleme     >>>>>\n---------------------------------------");
            Console.Write("Programın süresini (dakika) giriniz: ");
            try { programTime = int.Parse(Console.ReadLine()); if (programTime > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        machinePrograms.Add(new MachineProgram(programID, programName, programTime));
        Console.WriteLine("Yeni program başarıyla eklendi.\nDevam etmek için klavyeden bir tuşa basınız!");
        Console.ReadKey();
        for (int j = 0; j < machines.Count; j++)
        {
            machines[j].machinePrograms = machinePrograms;
        }
    }
    public void DeleteProgram()
    {
        int programID;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Program Silme     >>>>>\n---------------------------------------");
            Console.Write("Programın numarasını giriniz: ");
            try { programID = int.Parse(Console.ReadLine()); if (programID > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        for (int i = 0; i < machinePrograms.Count; i++)
        {
            if (machinePrograms[i].programID == programID)
            {
                machinePrograms.Remove(machinePrograms[i]);
                Console.WriteLine("Program başarıyla silindi.\nDevam etmek için klavyeden bir tuşa basınız!");
                Console.ReadKey();
                for (int j = 0; j < machines.Count; j++)
                {
                    machines[j].machinePrograms = machinePrograms;
                }
                break;
            }
            else if (i == machines.Count - 1)
            {
                Console.WriteLine("Böyle bir program bulunamadı!");
                Console.ReadKey();
            }
        }
    }
    public void FindAllPrograms()
    {
        Console.Clear();
        Console.WriteLine("<<<<<    Tüm Programlar     >>>>>\n---------------------------------------");
        if (machinePrograms.Count > 0)
        {
            for (int i = 0; i < machinePrograms.Count; i++)
            {
                Console.WriteLine("Program Numarası: " + machinePrograms[i].programID + "   Program İsmi: " + machinePrograms[i].programName + "    Program Süresi (dakika): " + machinePrograms[i].programTime);
            }
        }
        else
        {
            Console.WriteLine("Kayıtlı hiçbir program bulunamadı!");
        }
        Console.WriteLine("Devam etmek için klavyeden bir tuşa basınız!");
        Console.ReadKey();
    }

    public void StartProgram()
    {
        int programID, machineID;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Program Başlatma     >>>>>\n---------------------------------------");
            Console.Write("Programın numarasını giriniz: ");
            try { programID = int.Parse(Console.ReadLine()); if (programID > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Program Başlatma     >>>>>\n---------------------------------------");
            Console.Write("Makinenin numarasını giriniz: ");
            try { machineID = int.Parse(Console.ReadLine()); if (machineID > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        if (machines[machineID - 1].machineCase == 2)
        {
            workingCases.Add(new WorkingCase(machineID, programID, DateTime.Now));
            machines[machineID].machineCase = 1;
            DateTime dateTime = DateTime.Now.AddMinutes((machinePrograms[programID - 1].programTime));
            Console.WriteLine("Programınız başlatıldı. Tahmini bitiş süresi: {0}\nDevam etmek için klavyeden bir tuşa basınız!", dateTime.ToLongTimeString());
        }
        else if (machines[machineID - 1].machineCase == 1)
        {
            Console.WriteLine("Bu makine zaten çalışıyor!\nDevam etmek için klavyeden bir tuşa basınız!");
        }
        else if (machines[machineID - 1].machineCase == 0)
        {
            Console.WriteLine("Bu makine bozuk!\nDevam etmek için klavyeden bir tuşa basınız!");
        }
        Console.ReadKey();
    }
    public void FinishProgram()
    {
        int programID, machineID;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Program Bitirme     >>>>>\n---------------------------------------");
            Console.Write("Programın numarasını giriniz: ");
            try { programID = int.Parse(Console.ReadLine()); if (programID > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        while (true)
        {
            Console.Clear();
            Console.WriteLine("<<<<<    Program Bitirme     >>>>>\n---------------------------------------");
            Console.Write("Makinenin numarasını giriniz: ");
            try { machineID = int.Parse(Console.ReadLine()); if (machineID > 0) break; else Console.WriteLine("Lütfen pozitif değer giriniz!"); Console.ReadKey(); }
            catch (Exception e) { Console.WriteLine(e + "\nDevam etmek için klavyeden bir tuşa basınız!"); Console.ReadKey(); }
        }
        if (machines[machineID - 1].machineCase == 1)
        {
            for (int i = 0; i < workingCases.Count; i++)
            {
                if (workingCases[i].machineID == machineID && workingCases[i].programID == programID)
                {
                    workingCases.Remove(workingCases[i]);
                    Console.WriteLine("Program sona erdi!\nDevam etmek için klavyeden bir tuşa basınız!");
                }
            }
        }
        else if (machines[machineID - 1].machineCase == 2)
        {
            Console.WriteLine("Bu makine zaten boşta!\nDevam etmek için klavyeden bir tuşa basınız!");
        }
        else if (machines[machineID - 1].machineCase == 0)
        {
            Console.WriteLine("Bu makine bozuk!\nDevam etmek için klavyeden bir tuşa basınız!");
        }
        Console.ReadKey();
    }

    public void FindAllWorkingMachines()
    {
        Console.Clear();
        Console.WriteLine("<<<<<    Tüm Çalışan Makineler     >>>>>\n---------------------------------------");
        if (workingCases.Count > 0)
        {
            for (int i = 0; i < workingCases.Count; i++)
            {
                Console.WriteLine("Makine Numarası: " + machines[workingCases[i].machineID - 1].machineID + "   Program İsmi: " + machinePrograms[workingCases[i].programID - 1].programName + "    Program Süresi: " + machinePrograms[workingCases[i].programID - 1].programTime + " dakika" + "    Program Başlama Zamanı: " + workingCases[i].beginTime.ToShortTimeString());
            }
        }
        else
        {
            Console.WriteLine("Kayıtlı hiçbir çalışan makine bulunamadı!");
        }
        Console.WriteLine("Devam etmek için klavyeden bir tuşa basınız!");
        Console.ReadKey();
    }
}
