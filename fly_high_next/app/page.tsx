import Image from "next/image";
import { Button } from "@/components/ui/button"
import { ThemeToggle } from "./theme-toggle";

export default function Home() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-white dark:bg-black text-black dark:text-white transition-all duration-300">
        <div className="max-w-3xl text-center space-y-10">
            <h1 className="text-6xl fot-semibold">
                Next.js Dark Mode
            </h1>
            <p>
                Lorem ipsum dolor sit amet, consectetur adipisicing elit. Adipisci assumenda dolorem libero molestiae molestias, nisi. At culpa ducimus ipsum laudantium nobis non quae quibusdam quidem, repellat sapiente. Ipsam, nobis ratione?
            </p>

            <div>
                <Button variant="secondary" className="mr-2"> Button 1</Button>
                <Button className="mr-2 p-5 cursor-pointer"><a href="./About"> About </a></Button>
                <Button variant="secondary" className="mr-2"> Button 2</Button>
            </div>

            <ThemeToggle/>
        </div>
    </div>
  );
}
