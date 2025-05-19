import React from "react";
import "../css/Home.css";

export default function Home() {
    return (
        <div className="container-home">
            <h2>Desafio Tributo Justo - Pleno</h2>
            <p className="autor">Ricardo Ferreira</p>

            <div className="tecnologias">
                <h4>Front-end</h4>
                <ul>
                    <li>React (com TypeScript)</li>
                    <li>Axios</li>
                </ul>

                <h4>Back-end</h4>
                <ul>
                    <li>.NET 8 (ASP.NET Core Web API)</li>
                    <li>Entity Framework Core</li>
                    <li>SQLite</li>
                    <li>JWT (Json Web Token)</li>
                    <li>BCrypt.Net</li>
                    <li>CsvHelper</li>
                </ul>
            </div>
        </div>
    );
}
