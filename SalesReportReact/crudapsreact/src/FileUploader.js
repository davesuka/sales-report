import axios from 'axios';
 
import React,{Component} from 'react';
 
class App extends Component {
  
    baseUrl = "https://localhost:44371/api/sales";
    state = {
 
      // Initially, no file is selected
      selectedFile: null
    };
    
    // On file select (from the pop up)
    onFileChange = event => {
    
      // Update the state
      this.setState({ selectedFile: event.target.files[0] });
    
    };

    postSales = async(formData) =>{
        await axios.post(this.baseUrl, formData)
        .then(response => {
          window.location.reload();
        }).catch(error => {
          console.log(error);
        })
    }
    
    // On file upload (click the upload button)
    onFileUpload = () => {
    
      // Create an object of formData
      const formData = new FormData();
    
      // Update the formData object
      formData.append(
        "myFile",
        this.state.selectedFile,
        this.state.selectedFile.name
      );
      
      this.postSales(formData);     
      
    };
    
    // File content to be displayed after
    // file upload is complete
    fileData = () => {
    
      if (this.state.selectedFile) {
         
        return (
          <div>
            <h2>File Details:</h2>             
                <p>File Name: {this.state.selectedFile.name}</p>
                <p>File Type: {this.state.selectedFile.type}</p>
          </div>
        );
      } 
    };
    
    render() {
    
      return (
        <div>
            <h1>
              Load Sales File
            </h1>
            <br/>
            <div>
                <input type="file" onChange={this.onFileChange} />
                <button onClick={this.onFileUpload}>
                  Upload!
                </button>
            </div>
          {this.fileData()}
        </div>
      );
    }
  }
 
  export default App;