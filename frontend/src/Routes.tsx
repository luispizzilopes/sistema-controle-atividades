import { Route, Routes, BrowserRouter } from "react-router-dom";
import Login from "./pages/Login";
import Home from "./pages/Home";

export default function RoutesApp(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" Component={Login}/>
                <Route path="/home" Component={Home}/>
            </Routes>
        </BrowserRouter>
    );
}