import { Route, Routes, BrowserRouter } from "react-router-dom";
import Login from "./pages/Login";
import Home from "./pages/Home";
import RegisterUser from "./pages/RegisterUser";
import { PrivateRoute } from "./routes/PrivateRoute";

export default function RoutesApp(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" Component={Login}/>
                <Route path="/home" element={<PrivateRoute><Home/></PrivateRoute>}/>
                <Route path="/register" Component={RegisterUser}/>

                <Route path="*" element={<div>Error 404.</div>}/>
            </Routes>
        </BrowserRouter>
    );
}