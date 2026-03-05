const API_URL = process.env.NEXT_PUBLIC_API_URL;

export async function fetchWithAuth(endpoint: string, options: RequestInit = {}) {
    const config: RequestInit = {
        ...options,
        credentials: "include",
        headers: {
            "Content-Type": "application/json",
            ...options.headers,
        },
    };

    let response = await fetch(`${API_URL}${endpoint}`, config);

    if (response.status === 401) {
        const refreshResponse = await fetch(`${API_URL}/auth/refresh`, {
            method: "POST",
            credentials: "include",
        });

        if (refreshResponse.ok) {
            response = await fetch(`${API_URL}${endpoint}`, config);
        } else {
            if (typeof window !== "undefined") {
                window.location.href = "/";
            }
        }
    }

    return response;
}