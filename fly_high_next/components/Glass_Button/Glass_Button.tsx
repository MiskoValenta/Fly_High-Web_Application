import { ButtonHTMLAttributes, ReactNode } from 'react';

type ButtonProps = {
    children: ReactNode;
} & ButtonHTMLAttributes<HTMLButtonElement>;

export const Glass_Button = ({ children, ...props } : ButtonProps) => {

    return (
        <button className="" {...props}>
            {children}
        </button>
    );
}
