import type { Metadata } from "next";
import {Poppins, Rock_Salt} from "next/font/google";
import "./globals.css";
import { ThemeProvider } from "next-themes"
import { Navbar } from '@/components/Navbar/Navbar';
import { Footer } from '@/components/Footer/Footer';
import Background from "@/components/ui/Background";


const poppins = Poppins({
    style: ["normal"],
    weight: ["100", "400"],
    variable: "--font-poppins",
   subsets: ["latin"]
});

const rockSalt = Rock_Salt({
    weight: "400",
    variable: "--font-rock-salt",
    subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Fly High!",
  description: "Maturitní Práce",
};

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode }>) {
    return (
        <html lang="en" suppressHydrationWarning>
        <body className={`${poppins.variable} ${rockSalt.variable} antialiased`}>
        <ThemeProvider
            attribute="class"
            defaultTheme="system"
            enableSystem
            disableTransitionOnChange
        >
            <Background>
                <div className="app-root">
                    <Navbar />
                    <main className="site-main">
                        {children}
                    </main>
                    <Footer />
                </div>
            </Background>
        </ThemeProvider>
        </body>
        </html>
    );
}

