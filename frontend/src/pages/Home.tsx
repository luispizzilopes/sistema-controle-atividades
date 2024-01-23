import React, { useEffect } from "react";

import './styles/home.css';
import api from "../services/api";
import ButtonAppBar from "../components/ButtonAppBar";
import Footer from "../components/Footer";

export default function Home() {

    const teste = async () => {
        await api.get("api/Atividade")
            .then(resp => console.log(resp.data))
            .catch(error => console.error("erro" + error))
    }

    useEffect(() => {
        teste();
    }, [])

    return (
        <React.Fragment>
            <div className="page-home">
                <ButtonAppBar>
                    <div>teste</div>
                </ButtonAppBar>

                <Footer />
            </div>
        </React.Fragment>
    );
}