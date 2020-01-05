import React from 'react';
import axios from 'axios';
import {Component} from 'react'

export default class EtlApiGet extends Component {

    constructor(){
        super();
        this.state = {
            data: [],
            extract:"http://google.com",
            etl:"http://google.com",
        }
    }

    componentDidMount() {
        this.apiGetReviews();
    };

    handleOnChangeExtract = (e) => {
        this.setState({extract: e.target.value})
    }
    handleOnChangeEtl = (e) => {
        this.setState({etl: e.target.value})
    }

    apiGetReviews = () =>{
        axios.get(`http://localhost:5000/etl/api`)
        .then(res => {
            console.log(res);
            const reciveData = res.data;
            this.setState({data: reciveData});
        }).catch(error => {
            console.log(error)
        })
    }

    apiClear = () => {
        axios.delete(`http://localhost:5000/etl/api/clear`)
        .then(res => {
           console.log("Delete Clear")
           window.location.reload();
        }).catch(error => {
            console.log(error)
        })
    }

    apiLoad = () => {
        axios.post(`http://localhost:5000/etl/api/load`)
        .then(res => {
           console.log("Post Load")
           window.alert(res.data)
           window.location.reload();

        }).catch(error => {
            console.log(error)
        })
    }

    apiTransform = () => {
        axios.post(`http://localhost:5000/etl/api/transform`)
        .then(res => {
           console.log("Post Transform")
           window.alert(res.data)
        }).catch(error => {
            console.log(error)
        })
    }

    apiEtl = (url) => {
        axios.post("http://localhost:5000/etl/api/etl/", {url : url},{
                headers: {
                    'Content-Type': 'application/json;charset=utf-8',
                }
            }
        )
        .then(res => {
            res? console.log(res):
            console.log("Post Etl")
            window.alert(res.data)
            window.location.reload();
        }).catch(error => {
            console.log(error)
        })
    }

    apiExtract = (url) => {
        axios.post(`http://localhost:5000/etl/api/extract`, {url : url},{
                headers: {
                    'Content-Type': 'application/json;charset=utf-8',
                }
            }
        )
        .then(res => {
            res? console.log(res):
            window.alert(res.data)
            console.log("Post Extract")
        }).catch(error => {
            console.log(error)
        })
    }

    render() {
        return (

            <div  className="d-flex flex-column col-12 col-sm-12 col-md-10 col-lg-8 col-xl-8 flex-wrap">
                <div className="btn-group d-flex">
                    <button type="button" onClick={this.apiGetReviews} className="btn btn-primary btn">Get Reviews</button>
                    <button type="button" onClick={this.apiTransform} className="btn btn-primary btn">Transform</button>
                    <button type="button" onClick={this.apiLoad} className="btn btn-primary btn">Load</button>
                    <button type="button" onClick={this.apiClear} className="btn btn-primary btn">Clear</button>
                </div>

                <form className="d-flex flex-column col-12 flex-wrap">
                    <div className="d-flex flex-row align-items-center flex-wrap">
                        <label className="col-12 col-sm-2 col-form-label">Extract</label>
                        <div className="co-12  col-sm-8">
                            <input className="col-12" type="text" onChange={this.handleOnChangeExtract} className="form-control" placeholder="http://google.com"/>
                        </div>
                        <div className="col-12  col-sm-2">
                            <button className="col-12" type="button" onClick={() => this.apiExtract(this.state.extract)} className="btn btn-primary btn">Extract</button>
                        </div>
                    
                    </div>
                    <div className="d-flex flex-row align-items-center flex-wrap">
                        <label className="col-12 col-sm-2 col-form-label">Etl</label>
                        <div className="co-12  col-sm-8">
                            <input className="col-12" type="text" onChange={this.handleOnChangeEtl} className="form-control" placeholder="http://google.com"/>
                        </div>
                        <div className="col-12  col-sm-2">
                            <button className="col-12" type="button" onClick={() => this.apiEtl(this.state.etl)} className="btn btn-primary btn">Etl</button>
                        </div>
                    </div>
                </form>

                <table className="table table-dark col-12">
                <thead>
                    <tr>
                    <th scope="col">#</th>
                    <th scope="col">Date</th>
                    <th scope="col">Name</th>
                    <th scope="col">Rating</th>
                    <th scope="col">Title</th>
                    <th scope="col">Text</th>
                    </tr>
                </thead>
                <tbody>
                {this.state.data.map((entry, index) =>
                    <tr key={entry.toString()}>
                        <th scope="row">{index+1}</th>
                        <td >{entry.reviewDate}</td>
                        <td >{entry.reviewerName}</td>
                        <td >{entry.productRating}</td>
                        <td >{entry.reviewTitle}</td>
                        <td >{entry.reviewText}</td>
                    </tr>
                )}
                </tbody>
                </table>
            </div>
        )
      }
}