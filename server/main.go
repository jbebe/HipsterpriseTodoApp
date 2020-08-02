package main

import (
	"net/http"
	"os"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
	"github.com/goonode/mogo"
)

type TodoItem struct {
	AuthorEmail string `json:"author"`
	Content     string `json:"content"`
}

func fileExists(filename string) bool {
	info, err := os.Stat(filename)
	if os.IsNotExist(err) {
		return false
	}
	return !info.IsDir()
}

func main() {
	initDb()

	router := gin.Default()
	config := cors.DefaultConfig()
	config.AllowOrigins = []string{"http://127.0.0.1:8002", "http://localhost:8002"}
	router.Use(cors.New(config))

	router.GET("/login", func(c *gin.Context) {
		c.JSON(http.StatusOK, gin.H{"email": "juhasz.balint.bebe@gmail.com"})
	})

	router.GET("/todo/:email", func(c *gin.Context) {
		// create todo CRUD
		// wrap redis - mongodb cache
		email := c.Param("email")
		print(email)
		c.JSON(http.StatusOK, []TodoItem{
			TodoItem{
				AuthorEmail: email,
				Content:     "Test content",
			},
			TodoItem{
				AuthorEmail: email,
				Content:     "Test content 2",
			},
		})
	})

	router.POST("/todo/:email", func(c *gin.Context) {
		var todoItem TodoItem
		c.BindJSON(&todoItem)
	})

	// go run $env:GOROOT/src/crypto/tls/generate_cert.go --host 127.0.0.1
	if fileExists("cert.pem") && fileExists("key.pem") {
		router.RunTLS(":8001", "cert.pem", "key.pem")
	} else {
		router.Run(":8001")
	}
}

func initDb() {
	config := &mogo.Config{
		ConnectionString: "localhost",
		Database:         "mogotest",
	}
	connection, err := mogo.Connect(config)
	if err != nil {
		return
	}
	connection.Connect()
}
