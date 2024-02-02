import { useState } from "react";
import api from "../services/api";

export default async function auth() {
    const [resultAuthRequest, setResultAuthRequest] = useState(0);

    const token = sessionStorage.getItem("token");
    const expiration = sessionStorage.getItem("expiration");
    const session = sessionStorage.getItem("session");

    await api.get("/api/Auth")
        .then(resp => setResultAuthRequest(resp.status))
        .catch(error => {
            setResultAuthRequest(401);
            console.error(error)
        })

    const dateTime = new Date();
    const dateExpiration = expiration ? new Date(expiration) : null;

    if (session && session !== "") {
        if (token && token !== "" && token.length !== 0 && resultAuthRequest == 200) {
            if (dateExpiration && dateExpiration <= dateTime) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    } else {
        return false;
    }
}
