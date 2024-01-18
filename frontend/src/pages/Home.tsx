import React from "react";
import NavigateButtom from "../components/NavigateButtom";

import './styles/home.css'; 

export default function Home() {
    return (
        <React.Fragment>
            <div className="page-home">

                <div className="footer-home">
                    <NavigateButtom abaSelecionada="home"/>
                </div>
            </div>
        </React.Fragment>
    );
}