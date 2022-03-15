import React, { useEffect, useState, Component } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import FileUploader from './FileUploader';

import {Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap';


function App () {
  const baseUrl = "https://localhost:44371/api/sales";
  const[data, setData] = useState([]);


  const getSales=async()=>{
    await axios.get(baseUrl)
      .then(response => {
        setData(response.data);
      }).catch(error => {
          console.log(error);
      })
  }

  useEffect(()=> {
    getSales();
  }, [])

  return (
    <div className="App">
      <FileUploader/>
      <table className ="table table-bordered">
        <thead>
          <tr>
            <th>Deal Number</th>
            <th>Customer Name </th>
            <th>Dealership Name</th>
            <th>Vehicle</th>
            <th>Price</th>
            <th>Date</th>
          </tr>
        </thead>
        <tbody>
          {
            data.map(sale=> (
              <tr key={sale.dealNumber}>
                  <td>{sale.dealNumber}</td>
                  <td>{sale.customerName}</td>
                  <td>{sale.dealerShipName}</td>
                  <td>{sale.vehicle}</td>
                  <td>{"CAD$"+sale.price.toFixed(2) }</td>
                  <td>{sale.date}</td>
              </tr>

            ))
          }
        </tbody>
      </table>          
    </div>

    
  );
}

export default App;
