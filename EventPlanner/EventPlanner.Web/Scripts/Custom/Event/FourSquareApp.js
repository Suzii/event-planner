import React from 'react';
import {Autocomplete} from './Autocomplete';
import {SelectedPlace} from './SelectedPlace';

export class FourSquareApp extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedPlaces: this.props.preSelectedPlaces || []
    };

    this.addPlace = this.addPlace.bind(this);
    this.deletePlace = this.deletePlace.bind(this);
    this.constructQuery = this.constructQuery.bind(this);
    this.componentWillMount = this.componentWillMount.bind(this);
    this.render = this.render.bind(this);

  }

  //callback function for FourSquareApp component
  addPlace(event, data) { 
    var newPlace = data;
    //do not allow to add places multiple times
    if(this.state.selectedPlaces.find((place) => {return place.VenueId === newPlace.VenueId})) {
      console.log('Place ' + newPlace.VenueId + ' - ' + newPlace.Name + 'is already added!');
      return;
    }
    console.debug('Adding place: ' + newPlace.VenueId + ' ' + newPlace.Name);
    this.state.selectedPlaces.push(newPlace);
    this.setState({SelectedPlaces: this.state.SelectedPlaces});
  }

  //callback function for SelectedPlace component
  deletePlace(event, place) {
    var newPlaces = this.state.selectedPlaces;
    var index = newPlaces.indexOf(place);
    if(index >= 0) {
      newPlaces.splice(index, 1);
      this.setState({selectedPlaces: newPlaces});
    }
  }

  constructQuery(query, settings) {
    var city = $('#cityInput').val() || this.props.deletePlace;
    var queryObject = {query: query, city: city};
    settings.type = "GET";
    settings.contentType = "application/json; charset=UTF-8";
    settings.data = queryObject;

    return settings;
  }

  componentWillMount() {
    //TODO AJAX get for places in edit mode
    //TODO delete loading placeholder
  }
  
  render() {
    return (
      <div>
        <div className="form-group">
          <input type="text" id="cityInput" htmlFor="cityInput" className="form-control col-sm-2" placeholder="City..." defaultValue={this.props.defaultPlace} />
          <Autocomplete addCallback={this.addPlace} url={this.props.getDataURL} constructQueryCallback={this.constructQuery} />
        </div>
        <div className="row">
          {this.state.selectedPlaces.map((place, index) => { 
            return (
              <SelectedPlace key={place.VenueId} place={place} index={index} deleteCallback={this.deletePlace}/>
              )
          })}
        </div>
      </div>
    );
  }
}

FourSquareApp.propTypes = {
    getDataURL: React.PropTypes.string.isRequired,
    preSelectedPlaces: React.PropTypes.array,
    defaultPlace: React.PropTypes.string
  };

FourSquareApp.defaultProps = {
    defaultPlace: 'Brno'
  };
