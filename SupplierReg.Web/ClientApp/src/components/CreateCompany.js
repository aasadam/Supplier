import React, { Component } from 'react';
import { toast } from 'react-toastify';
import { Combobox } from 'react-widgets'

export class CreateCompany extends Component {
    static displayName = CreateCompany.name;

    constructor(props) {
        super(props);

        var states = [
            { uf: 'AC', name: 'AC - Acre' },
            { uf: 'AL', name: 'AL - Alagoas' },
            { uf: 'AP', name: 'AP - Amapá' },
            { uf: 'AM', name: 'AM - Amazonas' },
            { uf: 'BA', name: 'BA - Bahia' },
            { uf: 'CE', name: 'CE - Ceará' },
            { uf: 'DF', name: 'DF - Distrito Federal' },
            { uf: 'ES', name: 'ES - Espírito Santo' },
            { uf: 'GO', name: 'GO - Goiás' },
            { uf: 'MA', name: 'MA - Maranhão' },
            { uf: 'MT', name: 'MT - Mato Grosso' },
            { uf: 'MS', name: 'MS - Mato Grosso do Sul' },
            { uf: 'MG', name: 'MG - Minas Gerais' },
            { uf: 'PA', name: 'PA - Pará' },
            { uf: 'PB', name: 'PB - Paraíba' },
            { uf: 'PR', name: 'PR - Paraná' },
            { uf: 'PE', name: 'PE - Pernambuco' },
            { uf: 'PI', name: 'PI - Piauí' },
            { uf: 'RJ', name: 'RJ - Rio de Janeiro' },
            { uf: 'RN', name: 'RN - Rio Grande do Norte' },
            { uf: 'RS', name: 'RS - Rio Grande do Sul' },
            { uf: 'RO', name: 'RO - Rondônia' },
            { uf: 'RR', name: 'RR - Roraima' },
            { uf: 'SC', name: 'SC - Santa Catarina' },
            { uf: 'SP', name: 'SP - São Paulo' },
            { uf: 'SE', name: 'SE - Sergipe' },
            { uf: 'TO', name: 'TO - Tocantins' }
        ];

        this.state = {
            name: '',
            uf: '',
            cnpj: '',
            states: states
        };
    }

    

    changeHandler = e => {
        this.setState({ [e.target.name]: e.target.value })
    }

    submitHandler = e => {
        e.preventDefault();
        console.log(this.state);
        debugger;
        fetch('https://localhost:44307/companies', {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.state)
        })
            .then((response) => {
                debugger;
                const ok = response.ok;
                const data = response.json();
                return Promise.all([ok, data]).then(res => ({
                    ok: res[0],
                    data: res[1]
                }));
            })
            .then(responseJson => {
                debugger;
                if (responseJson.ok) {
                    toast.success("Empresa cadastrada com sucesso.");
                    this.setState({
                        name: '',
                        uf: '',
                        cnpj: ''
                    });
                } else {
                    if (responseJson.data != null) {
                        responseJson.data.map((error, i) => {
                            toast.error(error.message);
                        });
                    } else {
                        toast.error("Ocorreu um erro ao salvar empresa.");
                    }
                }
            })
            .catch((error) => {
                console.error(error);
            });
    }

    render() {
        const { name, uf, cnpj, states } = this.state

        var inputStyle = {
            float: 'right'
        };


        var buttonStyle = {
            paddingTop: '20px'
        };

        return (
            <div>
                <div>
                    <h1>Cadastrar Empresa</h1>
                </div>
                <br />
                <form onSubmit={this.submitHandler}>
                    <div className="row">
                        <div className="col-3 form-group">
                            <label>Name</label>
                            <input
                                className="form-control"
                                type="text"
                                name="name"
                                value={name}
                                style={inputStyle}
                                onChange={this.changeHandler}
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-3 form-group">
                            <label>UF</label>

                            <Combobox
                                data={states}
                                valueField='uf'
                                textField='name'
                                defaultValue={this.state.uf}
                                onChange={value => this.setState({ uf: value.uf })}
                            />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-3 form-group">
                            <label>CNPJ</label>
                            <input
                                className="form-control"
                                type="text"
                                name="cnpj"
                                value={cnpj}
                                style={inputStyle}
                                onChange={this.changeHandler}
                            />
                        </div>
                    </div>
                    <div className="row" style={buttonStyle}>
                        <div className="col-4">
                            <button className="btn btn-primary" type="submit" style={inputStyle}>Criar Empresa</button>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}
