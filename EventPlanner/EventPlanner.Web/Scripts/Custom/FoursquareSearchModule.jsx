/*
TODOS:
- templates for no result..
- loading indicator befor component did mount
*/

var SelectedPlace = React.createClass({
  propTypes: {
    place: React.PropTypes.object.isRequired,
    index: React.PropTypes.number,
    deleteCallback: React.PropTypes.func
  },
  render: function() {
    return (
      <div>
        <input type="hidden" value={this.props.place.Id} name={"Places["+this.props.index+"].Id"} /> 
        <input type="hidden" value={this.props.place.VenueId} name={"Places["+this.props.index+"].VenueId"} /> 
        <input type="hidden" value={this.props.place.Name} name={"Places["+this.props.index+"].Name"} /> 
        <input type="hidden" value={this.props.place.AddressInfo} name={"Places["+this.props.index+"].AddressInfo"} /> 
        <input type="hidden" value={this.props.place.City} name={"Places["+this.props.index+"].City"} /> 
        <h3>
          <span className="label label-warning">{this.props.place.Name}</span>
          <a><i className="glyphicon glyphicon-remove" onClick={(event) => {this.props.deleteCallback(event, this.props.place)}}></i></a>
        </h3>          
      </div>
    );
  }

});

var Autocomplete = React.createClass({
  propTypes: {
    url: React.PropTypes.string.isRequired,
    constructQueryCallback: React.PropTypes.func,
    addCallback: React.PropTypes.func,
  },
  getDefaultProps: function() {
    return {
      constructQueryCallback: function (query, settings) {
        console.warning('This default function should be overridden to best fit the use case.');
        queryObject = {q: query}; 
        settings.type = "POST";
        settings.contentType = "application/json; charset=UTF-8";
        settings.data = JSON.stringify(queryObject);

        return settings;
      },
      addCallback: function(event, data){}
    }
  },
  //object for autocomplete is item = {id: '0', Name: 'Some description'}
  componentDidMount: function() {
    var items = new Bloodhound({
      datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Name'),
      queryTokenizer: Bloodhound.tokenizers.whitespace,
      identify : function(item) {return item.VenueId},
      //everything is fetched from service - no local data provided
      local: [],  
      //where to get data for Bloodhound engine
      remote: {
        //URL for AJAX call
        url: this.props.url,
        //milliseconds to wait until service call id triggered,
        rateLimitWait: 500,   
        //prepare AJAX call to server - settings is jQuery promise object
        prepare: this.props.constructQueryCallback,
        //transformation od response object before Bloodhound engine operates on it
        transform: function(response) { 
          return response.map(function(item){ 
              return { 
                Id: item.Id,
                VenueId: item.VenueId,
                Name: item.Name,
                City: item.City,
                AddressInfo: item.AddressInfo
              }
            });
        }
      }      
    });
    var self = this;

    $('.typeahead').typeahead({
      hint: true,
      highlight: true,
      minLength: 3
    },
    {
      name: 'Query',
      source: items,
      displayKey: 'Name',
      templates: {
          suggestion: function(data){
            return '<div><p>' + data.Name + '</p><p class="text-muted">' + data.AddressInfo + '</p></div>';
          },
          empty: '<div class="empty-message text-danger"> No places found...</div>'
        },
    }).on('typeahead:selected', function(event, data){  //TODO add button which will trigger the add method itself?
        self.props.addCallback(event, data);
        $('.typeahead').typeahead('val', '');
        console.debug('Value selected: ' + data.VenueId + ' - ' + data.Name);  
    });
  },
  render: function() {
    return (
        <input type="text" id="queryInput" name="queryInput" className="typeahead form-control col-sm-3" placeholder="What do you wanna do?" />
      );
  }
});

var FourSquareApp = React.createClass({
  propTypes: {
    getDataURL: React.PropTypes.string.isRequired,
    preSelectedPlaces: React.PropTypes.array
  },
  getInitialState: function() {
    return {
      selectedPlaces: this.props.preSelectedPlaces || []
    };
  },
  //callback function for FourSquareApp component
  addPlace: function(event, data){ 
    var newPlace = data;
    //do not allow to add places multiple times
    if(this.state.selectedPlaces.find((place) => {return place.VenueId === newPlace.VenueId})){
      console.log('Place ' + place.VenueId + ' - ' + place.Name + 'is already added!');
      return;
    }
    console.debug('Adding place: ' + newPlace.VenueId + ' ' + newPlace.Name);
    this.state.selectedPlaces.push(newPlace);
    this.setState({SelectedPlaces: this.state.SelectedPlaces});
  },
  //callback function for SelectedPlace component
  deletePlace: function(event, place){
    var newPlaces = this.state.selectedPlaces;
    var index = newPlaces.indexOf(place);
    if(index >= 0) {
      newPlaces.splice(index, 1);
      this.setState({selectedPlaces: newPlaces});
    }
  },
  constructQuery: function (query, settings) {
    var city = $('#cityInput').val() || 'Brno';
    queryObject = {query: query, city: city};
    settings.type = "GET";
    settings.contentType = "application/json; charset=UTF-8";
    settings.data = queryObject;

    return settings;
  },
  componentWillMount: function() {
    //TODO AJAX get for places in edit mode
    //TODO delete loading placeholder
  },
  render: function() {
    return (
      <div>
        <div>
          {this.state.selectedPlaces.map((place, index) => { 
            return (
              <SelectedPlace key={place.VenueId} place={place} index={index} deleteCallback={this.deletePlace}/>
              )
          })}
        </div>
        <div className="form-group">
          <input type="text" id="cityInput" htmlFor="cityInput" className="form-control col-sm-2" placeholder="City..." />
          <Autocomplete addCallback={this.addPlace} url={this.props.getDataURL} constructQueryCallback={this.constructQuery}/>
        </div>
      </div>
    );
  }
});
