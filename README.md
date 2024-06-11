<h1>Booking</h1>

<p>Visoko ocenjeni akademski projekat iz predmeta Specifikacija i modeliranje softvera i Interakcija čovek računar na Fakultetu tehničkih nauka. Aplikacija <strong>Booking</strong> omogućava korisnicima da jednostavno rezervišu smeštaj i prateće usluge, pružajući intuitivno korisničko iskustvo za različite tipove korisnika.</p>

<h2>O Projektu</h2>

<p>Aplikacija <strong>Booking</strong> je WPF aplikacija koja prati principe čistog koda (clean code), dizajn paterne, kao i MVVM arhitekturu. Cilj aplikacije je da omogući rezervaciju smeštaja i pratećih usluga za različite tipove korisnika.</p>

<p>Aplikacija podržava četiri tipa korisnika:</p>
<ul>
    <li><strong>Vlasnik smeštaja</strong></li>
    <li><strong>Gost (posetilac smeštaja)</strong></li>
    <li><strong>Vodič</strong></li>
    <li><strong>Turista</strong></li>
</ul>

<p>Za potrebe logovanja, informacije o korisnicima mogu se pronaći u fajlu <code>Resources/Data/user.csv</code>.</p>

<h2>Implementacija</h2>

<p>Projekat implementira ključne softverske principe i tehnike, uključujući:</p>
<ul>
    <li><strong>Principi čistog koda (Clean Code)</strong></li>
    <li><strong>SOLID principi</strong></li>
    <li><strong>Dizajn paterne</strong></li>
    <li><strong>MVVM arhitektura</strong></li>
</ul>

<p>Aplikacija takođe sadrži četiri vrste dijagrama za vizualizaciju i bolji uvid u arhitekturu i tokove unutar sistema:</p>
<ul>
    <li><strong>Use Case dijagrami</strong></li>
    <li><strong>Class dijagrami</strong></li>
    <li><strong>Sequence dijagrami</strong></li>
    <li><strong>Activity dijagrami</strong></li>
</ul>

<h2>Struktura Projekta</h2>

<ul>
    <li><strong>Application</strong>: Sadrži servise podeljene u tri grupe:
        <ul>
            <li><strong>Feature Services</strong></li>
            <li><strong>Rate Services</strong></li>
            <li><strong>Reservation Services</strong></li>
        </ul>
    </li>
    <li><strong>Diagrams</strong>: Folder koji sadrži dijagrame slučajeva korišćenja, klasne dijagrame, dijagrame sekvenci i dijagrame aktivnosti.</li>
    <li><strong>Domain</strong>: Sadrži modele i interfejse za repozitorijume.</li>
    <li><strong>Injektor</strong>: Sadrži konfiguraciju za dependency injection.</li>
    <li><strong>LocalisationResources</strong>: Sadrži resurse za lokalizaciju.</li>
    <li><strong>Observer</strong>: Implementacija observer paterna.</li>
    <li><strong>Properties</strong>: Sadrži konfiguracione fajlove.</li>
    <li><strong>Repository</strong>: Sadrži repozitorijume podeljene u tri grupe:
        <ul>
            <li><strong>Feature Repositories</strong></li>
            <li><strong>Rate Repositories</strong></li>
            <li><strong>Reservation Repositories</strong></li>
        </ul>
    </li>
    <li><strong>Resources</strong>: Sadrži podatke i PDF primere:
        <ul>
            <li><strong>Data</strong>: CSV fajlovi sa podacima.</li>
            <li><strong>PDF Examples</strong>: Primeri generisanih PDF fajlova.</li>
        </ul>
    </li>
    <li><strong>Serializer</strong>: Sadrži logiku za serijalizaciju podataka.</li>
    <li><strong>Styles</strong>: Svi stilovi korišćeni tokom cele aplikacije.</li>
    <li><strong>Utilites</strong>: PDF generator i pomoćne funkcionalnosti za menjanje tema.</li>
    <li><strong>WPF</strong>: Glavni folder za WPF resurse i komponente:
        <ul>
            <li><strong>Resources/Images</strong>: Sadrži slike korišćene u aplikaciji.</li>
            <li><strong>Windows</strong>: Glavni prozori aplikacije.</li>
            <li><strong>Pages</strong>: Stranice aplikacije.</li>
            <li><strong>User Controls</strong>: Korisnički kontroleri, podeljeni po ulogama.</li>
            <li><strong>ViewModels</strong>: View modeli za MVVM arhitekturu.</li>
        </ul>
    </li>
</ul>

<h2>Korišćeni alati</h2>

<ul>
    <li><strong>Microsoft Visual Studio</strong></li>
    <li><strong>Balsamiq Wireframes</strong></li>
</ul>

<h2>Korišćeni NuGet Paketi</h2>

<ul>
    <li><strong>DotNetProjects.WpfToolkit.DataVisualization</strong> (v6.1.94)</li>
    <li><strong>Extended.Wpf.Toolkit</strong> (v4.6.0)</li>
    <li><strong>FontAwesome.Sharp</strong> (v6.3.0)</li>
    <li><strong>iTextSharp</strong> (v5.5.13.3)</li>
    <li><strong>LiveCharts</strong> (v0.9.7)</li>
    <li><strong>LiveCharts.WinForms</strong> (v0.9.7.1)</li>
    <li><strong>Microsoft.Xaml.Behaviors.Wpf</strong> (v1.1.77)</li>
    <li><strong>MVVMLight.Messaging</strong> (v1.0.0)</li>
    <li><strong>MvvmLightLibs</strong> (v5.4.1.1)</li>
    <li><strong>LiveCharts.Wpf</strong> (v0.9.7)</li>
    <li><strong>OxyPlot.Pdf</strong> (v2.1.2)</li>
    <li><strong>OxyPlot.SkiaSharp</strong> (v2.1.2)</li>
    <li><strong>QuestPDF</strong> (v2024.3.10)</li>
    <li><strong>Syncfusion.Pdf.NET</strong> (v25.2.3)</li>
    <li><strong>WPF-UI</strong> (v3.0.4)</li>
</ul>

<h2>Prilagođavanje Aplikacije Korisnicima</h2>

<h3>Vodič</h3>
<p>Deo aplikacije za vodiča je dizajniran za korisnika koji je u ranim 30-im godinama i kojem je potrebna automatizacija poslovnih procesa. Ova sekcija uključuje funkcionalnosti kao što su upravljanje turama, praćenje rezervacija i komunikacija sa turistima.</p>

<h3>Turista</h3>
<p>Deo aplikacije za turiste je prilagođen starijim osobama koje se ne snalaze dobro sa računarima i plaše se da ne naprave grešku. Interfejs je jednostavan, sa jasnim uputstvima i velikim dugmadima za lakšu navigaciju.</p>

<h3>Vlasnik smeštaja</h3>
<p>Deo aplikacije za vlasnika smeštaja je prilagođen potrebama iskusnih osoba koje imaju ograničeno vreme. Ova sekcija omogućava jednostavno upravljanje rezervacijama, pregled statistike o smeštaju i komunikaciju sa gostima.</p>

<h3>Gost</h3>
<p>Deo aplikacije za gosta je prilagođen osobi koja se tek navikava na rad sa računarom i oslanja se na sisteme pomoći. Interfejs pruža intuitivnu navigaciju i jasne instrukcije za korišćenje.</p>

<h2>Autori</h2>

<ul>
    <li><strong>Teodora Bečejac, RA 37/2021</strong></li>
    <li><strong>Katarina Petrović, RA 17/2021</strong></li>
    <li><strong>Nataša Radmilović, RA 20/2021</strong></li>
    <li><strong>Akoš Kiš, RA 11/2021</strong></li>
</ul>

<h2>Galerija Screenshotova Aplikacije</h2>

<p>Ovde možete videti izgled glavnih delova aplikacije podeljene po ulogama.</p>

<img src="Screenshots/login.png" alt="Prozor za prijavu" width="670">

<h3>Vodič</h3>
<img src="Screenshots/guidemain.png" alt="Glavni prozor vodiča" width="670">
<img src="Screenshots/guidestats.png" alt="Stranica statistike tura vodiča" width="670">

<h3>Vlasnik smeštaja</h3>
<img src="Screenshots/hostmain.png" alt="Početna stranica vlasnika smeštaja" width="670">
<img src="Screenshots/hoststats.png" alt="Stranica statistike smeštaja vlasnika" width="670">

<h3>Gost</h3>
<img src="Screenshots/guestmain.png" alt="Početna stranica gosta - svetla tema" width="670">
<img src="Screenshots/guestmaindark.png" alt="Početna stranica gosta - tamna tema" width="670">
<img src="Screenshots/guestdelay.png" alt="Stranica zahteva za odlaganje gosta - tamna tema" width="670">

<h3>Turista</h3>
<img src="Screenshots/touristmain.png" alt="Početna stranica turista" width="670">
<img src="Screenshots/touristrequests.png" alt="Stranica zahteva turista" width="670">
