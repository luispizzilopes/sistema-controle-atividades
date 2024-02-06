import React, { useState } from "react";
import OutlinedCard from "../components/Card";
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { toast } from "react-toastify";
import { ILogin } from "../interfaces/ILogin";
import DialogPassword from "../components/DialogPassword";
import { encryptText } from "../Encrypt/Encrypt";
import { IconButton } from "@mui/material";
import { Visibility } from "@mui/icons-material";
import { VisibilityOff } from "@mui/icons-material";
import logoExtendFile from '../assets/extendfile_branco.png';
import logoSistemaAtividade from '../assets/sistema-atividades.png'

import './styles/login.css';
import LoadingProgress from "../components/LoadingProgress";

export default function Login() {
    const [email, setEmail] = useState<String>("");
    const [password, setPassword] = useState<String>("");
    const [openModal, setOpenModal] = useState<boolean>(false);

    const [loading, setLoading] = useState<boolean>(false);
    const [showPassword, setShowPassword] = useState<boolean>(false);

    const handleTogglePasswordVisibility = () => {
        setShowPassword(!showPassword);
    };

    const navigate = useNavigate();

    const submitRequestLogin = async () => {
        if (email != "" && password != "") {
            setLoading(true);
            let bodyLogin: ILogin = {
                email,
                password
            };

            axios.post("https://localhost:7001/api/Auth/login", bodyLogin)
                .then(resp => {
                    if (resp.status === 200 && resp.data.authenticated === true) {
                        sessionStorage.setItem("token", resp.data.token);
                        sessionStorage.setItem("expiration", resp.data.expiration);
                        sessionStorage.setItem("session", encryptText(JSON.stringify(resp.data)))

                        navigate("/home")
                    } else {
                        toast.success("Não foi possível realizar o Login!");
                        setLoading(false);
                    }
                })
                .catch(error => {
                    setLoading(false);
                    toast.error(error.response.data.Erro[0]);
                });
        }
    }

    return (
        <React.Fragment>
            <div className="page-login">
                <OutlinedCard>
                    <div className="logo-task">
                        <img src={logoSistemaAtividade} style={{
                            width: "250px",
                            height: "250px",
                            marginBottom: "10px"
                        }} />
                    </div>
                    <div className="form-login">
                        <TextField
                            label="Usuário ou E-mail:"
                            variant="outlined"
                            value={email}
                            onChange={e => setEmail(e.target.value)} />

                        <TextField
                            label="Senha:"
                            variant="outlined"
                            type={showPassword ? 'text' : 'password'}
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            InputProps={{
                                endAdornment: (
                                    <IconButton onClick={handleTogglePasswordVisibility}>
                                        {showPassword ? <Visibility /> : <VisibilityOff />}
                                    </IconButton>
                                ),
                            }}
                        />

                        <small
                            onClick={() => setOpenModal(true)}
                            style={{
                                textDecoration: "underline",
                                cursor: "pointer",
                                textAlign: "end"
                            }}>Esqueceu sua senha?</small>

                        <Button
                            variant="contained"
                            onClick={() => submitRequestLogin()}>
                            Acessar
                        </Button>
                        <Button variant="contained"
                            onClick={() => navigate("register")}>
                            Registrar-se
                        </Button>
                    </div>

                    <div className="extend-file-div">
                        <img src={logoExtendFile} alt="Logo eXtend File" width={150} />
                        <small id="extend-file">{new Date().getFullYear().toString()} Copyright © - Desenvolvido por eXtend File.</small>
                    </div>

                </OutlinedCard>
            </div>

            <LoadingProgress loading={loading} />
            
            <DialogPassword open={openModal} setOpen={setOpenModal} />
        </React.Fragment>
    );
}