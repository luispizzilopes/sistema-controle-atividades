import { Route, Routes, BrowserRouter } from "react-router-dom";
import Login from "./pages/Login";

export default function RoutesApp(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" Component={Login}/>
            </Routes>
        </BrowserRouter>
    );
}