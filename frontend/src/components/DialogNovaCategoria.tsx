import React, { useState } from 'react';
import Button from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import { TextField } from '@mui/material';
import api from '../services/api';
import { toast } from 'react-toastify';
import { decryptText } from '../Encrypt/Encrypt';

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
    '& .MuiDialogContent-root': {
        padding: theme.spacing(2),
    },
    '& .MuiDialogActions-root': {
        padding: theme.spacing(1),
    },
}));

interface INovaCategoria {
    open: boolean;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
    carregarCategorias : React.Dispatch<React.SetStateAction<void>>;
}

export default function DialogNovaCategoria({ open, setOpen, carregarCategorias }: INovaCategoria) {
    const [nomeCategoria, setNomeCategoria] = useState<String>("");
    const [descricaoCategoria, setDescricaoCategoria] = useState<String>("");

    const adicionarNovaCategoria = async () => {
        if(nomeCategoria != "" && descricaoCategoria != ""){
            await api.post("api/Categoria", {
                nomeCategoria: nomeCategoria,
                userId: JSON.parse(decryptText(sessionStorage.getItem("session")!)).id, 
                descricaoCategoria: descricaoCategoria,
                dataCriacaoCategoria: new Date()
            })
                .then(resp => {
                    toast.success(resp.data);
                    carregarCategorias(); 
                    setOpen(false);
                })
                .catch(error => {
                    toast.error("Erro ao cadastrar uma nova categoria!");
                    console.error(error);
                })
        }else{
            toast.warn("Verifique todos os campos e tente novamente!");
        }
    }

    return (
        <React.Fragment>
            <BootstrapDialog
                onClose={() => setOpen(false)}
                aria-labelledby="customized-dialog-title"
                open={open}
            >
                <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
                    Nova Categoria
                </DialogTitle>
                <IconButton
                    aria-label="close"
                    onClick={() => setOpen(false)}
                    sx={{
                        position: 'absolute',
                        right: 8,
                        top: 8,
                        color: (theme) => theme.palette.grey[500],
                    }}
                >
                    <CloseIcon />
                </IconButton>
                <DialogContent dividers>

                    <p>Nome da Categoria</p>
                    <TextField
                        value={nomeCategoria}
                        onChange={e => setNomeCategoria(e.target.value)}
                        sx={{ width: "320px" }}
                        inputProps={{ maxLength: 50 }} />

                    <p>Descrição da Categoria</p>
                    <TextField
                        value={descricaoCategoria}
                        onChange={e => setDescricaoCategoria(e.target.value)}
                        multiline
                        rows={5}
                        sx={{ width: "320px" }}
                        inputProps={{ maxLength: 255 }} />

                </DialogContent>
                <DialogActions>
                    <Button autoFocus onClick={() => adicionarNovaCategoria()} >
                        Adicionar Categoria
                    </Button>
                </DialogActions>
            </BootstrapDialog>
        </React.Fragment>
    );
}