export default function auth() {
    const token = sessionStorage.getItem("token");
    const expiration = sessionStorage.getItem("expiration");

    const dateTime = new Date();
    const dateExpiration = expiration ? new Date(expiration) : null;

    if (token && token !== "" && token.length !== 0) {
        if (dateExpiration && dateExpiration <= dateTime) {
            return false; 
        } else {
            // Lógica a ser executada quando o token estiver válido
            return true; 
        }
    } else {
        return false; 
    }
}
