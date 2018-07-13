import Html exposing (Html, button, div, text)
import Html.Attributes exposing (class)
import Html.Events exposing (onClick)

import List



main =
  Html.beginnerProgram
    { model = model
    , view = view
    , update = update
    }


-- MODEL

-- type alias Model = Int
type alias Model = 
    {
      firstCombinaison : (List String),
      combinaison : (List String),
      numberOfTry : Int,
      result : (List String),
      try : (List String)
    }

type alias Colors = 
    {
      red : String,
      green : String,
      blue : String,
      pink : String,
      orange : String,
      yellow : String
    }

-- Il faut créer un modèle qui correspond à notre jeu 
-- Avec la combinaison gagnante, le nombre d'essai restant
-- et le résultat de fiches blanches ou noires
model : Model
model = 
  {
    firstCombinaison = [],
    numberOfTry = 0,
    combinaison = [],
    result = [],
    try = []
  }

couleur : Colors
couleur = 
  {
    blue = "blue",
    red = "red",
    orange = "orange",
    green = "green",
    pink = "pink",
    yellow = "yellow"
  }

-- type alias Player =
--     { id : Int
--     , name : String
--     }


-- UPDATE

  
type Msg
  = SubmitTry (List String)
  | SubmitCombinaison (List String)
  | Reset
  -- | Red
  -- | Blue
  -- | Yellow
  -- | Pink
  -- | Orange
  -- | Green


update : Msg -> Model -> Model
update msg model =
  case msg of
    SubmitCombinaison list ->
     { model | combinaison = list }

    SubmitTry list ->
      { model | try = list }

    Reset -> 
      { model | try = [] }

-- update : Msg -> Colors -> Colors 
-- update msg couleur = 
--   case msg of 
--     Blue -> 
--       {model | "blue" :: firstCombinaison}


-- VIEW


view : Model -> Html Msg
view model =
  div []
    [
      div [] 
        [  
          div [class "myClass"]
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
          ,div []
            [ text "0 ", text "0 ", text "0 ", text "0 ", text "0 " ]
        ]
      ,div []
        [
          div []
              [ text "Choissisez votre combinaison"]
          -- ,div []
              -- [ button [ onClick Blue ] [text "0 "], text "0 ", text "0 ", text "0 ", text "0 " ]
        ]
      ,div []
        [
          --  button [ onClick SubmitCombinaison ] [text "Soumettre la combinaison"],
           button [ onClick Reset ] [text "Remettre à 0"]
        ]
    ]
