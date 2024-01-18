import React, { useState } from "react";
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import TaskAltIcon from '@mui/icons-material/TaskAlt';

import './styles/login.css';

export default function Login() {
    const [email, setEmail] = useState<String>(""); 
    const [password, setPassword] = useState<String>(""); 

    const submitRequestLogin = async()=>{
        console.log({
            email,
            password
        })
    }

    return (
        <React.Fragment>
            <div className="page-login">
                <div className="logo-task">
                    <TaskAltIcon sx={{
                        width: "150px",
                        height: "150px",
                        marginBottom: "20px"
                    }} />
                </div>
                <div className="form-login">
                    <TextField 
                        label="E-mail:" 
                        variant="outlined" 
                        value={email}
                        onChange={e=> setEmail(e.target.value)}/>

                    <TextField 
                        label="Senha:" 
                        variant="outlined" 
                        type="password" 
                        value={password}
                        onChange={e=> setPassword(e.target.value)}/>

                    <Button 
                        variant="contained"
                        onClick={()=> submitRequestLogin()}>
                        Acessar
                    </Button>
                    <Button variant="contained">Registrar-se</Button>
                </div>

                <small>2024 Copyright © - Desenvolvido por Luis Felipe.</small>
            </div>
        </React.Fragment>
    );
}