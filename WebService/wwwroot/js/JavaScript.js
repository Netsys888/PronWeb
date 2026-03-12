async function callApi(url, options = {}) {

    let token = localStorage.getItem("accessToken");

    options.headers = options.headers || {};

    options.headers["Authorization"] = "Bearer " + token;

    let res = await fetch(url, options);

    // token หมดอายุ
    if (res.status === 401) {

        const refreshed = await refreshToken();

        if (refreshed) {

            token = localStorage.getItem("accessToken");

            options.headers["Authorization"] = "Bearer " + token;

            res = await fetch(url, options);

        }
        else {

            window.location = "/Home/Login";
            return;

        }

    }

    return res;
}

async function refreshToken() {

    const refreshToken = localStorage.getItem("refreshToken");

    if (!refreshToken) return false;

    const res = await fetch("https://localhost:5000/auth/refresh", {

        method: "POST",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify({
            refreshToken: refreshToken
        })

    });

    if (!res.ok) {
        return false;
    }

    const data = await res.json();

    localStorage.setItem("accessToken", data.accessToken);
    localStorage.setItem("refreshToken", data.refreshToken);

    return true;
}