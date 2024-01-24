import React, { useEffect, useState } from "react";

import './styles/home.css';
import api from "../services/api";
import ButtonAppBar from "../components/ButtonAppBar";
import Footer from "../components/Footer";
import OutlinedCard from "../components/Card";

import WorkIcon from '@mui/icons-material/Work';
import CategoryIcon from '@mui/icons-material/Category';
import AccessTimeFilledIcon from '@mui/icons-material/AccessTimeFilled';
import TimelineIcon from '@mui/icons-material/Timeline';
import IHomeInfo from "../interfaces/IHomeInfo";
import Chart from "../components/Chart";

export default function Home() {
    type HomeInfo = IHomeInfo | null;
    const [info, setInfo] = useState<HomeInfo>(null);

    const loadingInfoHome = async () => {
        await api.get("api/Home")
            .then(resp => setInfo(resp.data))
            .catch(error => console.log(error));
    }

    useEffect(() => {
        loadingInfoHome();
    }, [])

    return (
        <React.Fragment>
            <div className="page-home">
                <ButtonAppBar>
                    <div>
                        <h1>Dashboard</h1>
                        <div className="home-cards">
                            <OutlinedCard>
                                <WorkIcon />
                                <p>Atividades Cadastradas</p>
                                <h1>{info?.atividadesCadastradas}</h1>
                                <small>Quantidade de Atividades cadastradas no mês.</small>
                            </OutlinedCard>
                            <OutlinedCard>
                                <CategoryIcon />
                                <p>Categorias Cadastradas</p>
                                <h1>{info?.categoriasCadastradas}</h1>
                                <small>Quantidade de Categorias cadastradas no mês.</small>
                            </OutlinedCard>
                        </div>
                        <div className="home-cards" style={{ marginTop: "10px" }}>
                            <OutlinedCard>
                                <AccessTimeFilledIcon />
                                <p>Tempo Total</p>
                                <h1>{info?.tempoTotalAtividades}h</h1>
                                <small>Tempo utilizado para realizar as atividades no mês.</small>
                            </OutlinedCard>
                            <OutlinedCard>
                                <TimelineIcon />
                                <p>Total Geral Atividades</p>
                                <h1>{info?.totalGeralAtividades}</h1>
                                <small>Quantidade de Atividades cadastradas desde o início.</small>
                            </OutlinedCard>
                        </div>
                        <Chart graficoAtividades={info?.graficoAtividades ?? []} graficoCategorias={info?.graficoCategorias ?? []} />
                    </div>
                </ButtonAppBar>

                <Footer />
            </div>
        </React.Fragment>
    );
}