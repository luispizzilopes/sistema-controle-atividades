import { Route, Routes, BrowserRouter } from "react-router-dom";
import Login from "./pages/Login";
import Home from "./pages/Home";
import RegisterUser from "./pages/RegisterUser";
import Error from "./pages/Error404";
import { PrivateRoute } from "./routes/PrivateRoute";
import Atividades from "./pages/Atividades";
import NovaAtividade from "./pages/NovaAtividade";

export default function RoutesApp(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" Component={Login}/>
                <Route path="/home" element={<PrivateRoute><Home/></PrivateRoute>}/>
                <Route path="/atividades" element={<PrivateRoute><Atividades/></PrivateRoute>}/>
                <Route path="/nova-atividade" element={<PrivateRoute><NovaAtividade/></PrivateRoute>}/>
                <Route path="/register" Component={RegisterUser}/>

                <Route path="*" element={<Error/>}/>
            </Routes>
        </BrowserRouter>
    );
}