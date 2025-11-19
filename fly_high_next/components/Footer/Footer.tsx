import { IoLogoInstagram, IoLogoChrome, IoLogoPolymer } from "react-icons/io";
import { IoLogoFacebook } from "react-icons/io5";
import Link from "next/link";
import Image from "next/image";
import "./Footer.css";
import Logo from "@/public/volleyball_logo_inverted.svg";

export const Footer = () => {

    return(
        <footer>
            <Image width={30} height={30} className="Logo" src={Logo} alt="Logo"/>
            <div className="socials">
                <a href="https://www.instagram.com/oaasoslopava"><IoLogoInstagram/></a>
                <a href="https://www.facebook.com/oa.sosl.opava"><IoLogoFacebook/></a>
                <a href="https://open.spotify.com/show/7oLGNL97NPOdC2cLRbrtAu"><IoLogoPolymer className="rotate-90"></IoLogoPolymer></a>
                <a href="https://www.oa-opava.cz"><IoLogoChrome/></a>
            </div>
            <div className="links">
                <ul>
                    <li>
                        <Link href="#">
                            Home
                        </Link>
                    </li>
                    <li>
                        <Link href="#">
                            About
                        </Link>
                    </li>
                    <li>
                        <Link href="#">
                            Contact
                        </Link>
                    </li>
                </ul>
            </div>
        </footer>
    )
}