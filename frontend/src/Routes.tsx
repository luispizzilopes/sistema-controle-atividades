import { Route, Routes, BrowserRouter } from "react-router-dom";
import Login from "./pages/Login";
import Home from "./pages/Home";
import RegisterUser from "./pages/RegisterUser";
import Error from "./pages/Error404";
import { PrivateRoute } from "./routes/PrivateRoute";
import Atividades from "./pages/Atividades";
import NovaAtividade from "./pages/NovaAtividade";
import Atividade from "./pages/Atividade";
import Categorias from "./pages/Categorias";
import Categoria from "./pages/Categoria";

export default function RoutesApp(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" Component={Login}/>
                <Route path="/home" element={<PrivateRoute><Home/></PrivateRoute>}/>
                <Route path="/atividades" element={<PrivateRoute><Atividades/></PrivateRoute>}/>
                <Route path="/nova-atividade" element={<PrivateRoute><NovaAtividade/></PrivateRoute>}/>
                <Route path="/atividade/:id" element={<PrivateRoute><Atividade/></PrivateRoute>}/>
                <Route path="/categorias" element={<PrivateRoute><Categorias/></PrivateRoute>}/>
                <Route path="/categoria/:id" element={<PrivateRoute><Categoria/></PrivateRoute>}/>
                <Route path="/register" Component={RegisterUser}/>

                <Route path="*" element={<Error/>}/>
            </Routes>
        </BrowserRouter>
    );
}